using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SanaApp.WEB.Models
{
    public class ProductsVirtual
    {
        public List<Products> GetProducts()
        {
            HttpContext context = HttpContext.Current;
            if (!UsingSessiondB())
            {
                SanaAppWEBContext ctx = new SanaAppWEBContext();
                return ctx.Products.ToList();
            }
            else
                return GetSessionProducts();
        }

        public Products SaveProduct(Products p)
        {
            HttpContext context = HttpContext.Current;

            if (!UsingSessiondB())
            {
                SanaAppWEBContext ctx = new SanaAppWEBContext();
                ctx.Products.Add(p);
                ctx.SaveChanges();
                return p;
            }
            else
            {
                p.ProductID = NextSessionKey();

                List<Products> Products = GetSessionProducts();

                Products.Add(p);

                context.Session["Products"] = Products;
                SetSessionKey(p.ProductID.ToString());
                return p;
            }
        }

        public Products EditProduct(Products p)
        {
            HttpContext context = HttpContext.Current;

            if (!UsingSessiondB())
            {
                SanaAppWEBContext ctx = new SanaAppWEBContext();
                ctx.Entry(p).State = EntityState.Modified;
                ctx.SaveChanges();
                return p;
            }
            else
            {
                List<Products> Products = GetSessionProducts();

                var index = Products.FindIndex((prod => prod.ProductID == p.ProductID));
                Products[index] = p;

                context.Session["Products"] = Products;
                SetSessionKey(p.ProductID.ToString());
                return p;
            }
        }

        public Products DeleteProduct(int id)
        {
            HttpContext context = HttpContext.Current;

            Products p = GetProduct(id);

            if (!UsingSessiondB())
            {
                SanaAppWEBContext ctx = new SanaAppWEBContext();
                ctx.Products.Attach(p);
                ctx.Products.Remove(p);
                ctx.SaveChanges();
                return p;
            }
            else
            {
                List<Products> Products = GetSessionProducts();

                Products.Remove(p);
                context.Session["Products"] = Products;
                return p;
            }
        }

        public Products GetProduct(int id)
        {
            if (UsingSessiondB())
            {
                List<Products> Products = GetSessionProducts();
                return Products.Find(p => p.ProductID == id);
            }
            else
            {
                SanaAppWEBContext ctx = new SanaAppWEBContext();
                return ctx.Products.Find(id);
            }
        }

        public List<Products> GetSessionProducts()
        {
            List<Products> Products = new List<Products>();

            HttpContext context = HttpContext.Current;

            //Check for session data
            if (context.Session["Products"] != null)
                Products = context.Session["Products"] as List<Products>;
            return Products;
        }

        private int NextSessionKey()
        {
            HttpContext context = HttpContext.Current;

            if (context.Session["LastSessionKey"] != null)
                return (Convert.ToInt32(context.Session["LastSessionKey"])) + 1;
            else
                return 0;
        }

        private void SetSessionKey(string lastsessionkey)
        {
            HttpContext context = HttpContext.Current;
            context.Session["LastSessionKey"] = lastsessionkey;
        }

        private bool UsingSessiondB()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["DataBase"].ToString() == "DataBase")
                return false; 
            else
                return true;
        }
    }
}