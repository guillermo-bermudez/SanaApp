using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SanaApp.WEB.Models;
using SanaApp.WEB.VirtualModel;

namespace SanaApp.WEB.Controllers
{
    public class ProductsController : Controller
    {
        private SanaAppWEBContext db = new SanaAppWEBContext();

        // GET: Pruducts
        public ActionResult Index()
        {
            return View(BindProductsListViewModel());
        }

        private ProductsListVirtualModel BindProductsListViewModel()
        {
            ProductsListVirtualModel vm = new ProductsListVirtualModel();

            string database = GetSessionDataBase();

            List<Products> Products = new List<Products>();

            ProductsVirtual vP = new ProductsVirtual();
            Products = vP.GetProducts();

            List<ProductsVirtualModel> empVirtualModels = new List<ProductsVirtualModel>();

            foreach (Products prd in Products)
            {
                ProductsVirtualModel prdViewModel = new ProductsVirtualModel();
                prdViewModel.ProductTitle = prd.ProductTitle;
                prdViewModel.ProductNumber = prd.ProductNumber;
                prdViewModel.ProductPrice = prd.ProductPrice;
                prdViewModel.ProductID = prd.ProductID;
                empVirtualModels.Add(prdViewModel);
            }

            vm.Products = empVirtualModels;
            vm.dB = database;
            return vm;
        }

        // GET: Pruducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Products products = db.Products.Find(id);
            if (products == null)
                return HttpNotFound();
            return View(products);
        }

        // GET: Pruducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pruducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductNumber,ProductTitle,ProductPrice")] Products products)
        {
            if (ModelState.IsValid)
            {
                ProductsVirtual vP = new ProductsVirtual();
                vP.SaveProduct(products);
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Pruducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ProductsVirtual vP = new ProductsVirtual();
            Products products = vP.GetProduct(Convert.ToInt32(id));
            if(products == null)
                return HttpNotFound();
            return View(products);
        }

        // POST: Pruducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductNumber,ProductTitle,ProductPrice")] Products products)
        {
            if (ModelState.IsValid)
            {
                ProductsVirtual vP = new ProductsVirtual();
                vP.EditProduct(products);
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Pruducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsVirtual vP = new ProductsVirtual();
            Products products = vP.GetProduct(Convert.ToInt32(id));
            if (products == null)
                return HttpNotFound();
            return View(products);
        }

        // POST: Pruducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductsVirtual vP = new ProductsVirtual();
            vP.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        private bool UsingSessionDB()
        {
            if (Session["DataBase"] != null)
            {
                if (Session["DataBase"].ToString() == "DataBase")
                    return false; //Not Session
                else
                    return true; //Session
            }
            else
            {
                SetSessionDataBase("Session");
                return true; 
            }
        }

        private void SetSessionDataBase(string db)
        {
            Session["DataBase"] = db;
        }

        private string GetSessionDataBase()
        {
            if (Session["DataBase"] != null)
                return Session["DataBase"].ToString();
            SetSessionDataBase("Session");
            return "Session";
        }
    }
}
