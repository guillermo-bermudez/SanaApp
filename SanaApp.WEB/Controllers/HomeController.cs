using SanaApp.WEB.VirtualModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace SanaApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "HOME";
            if (Session["DataBase"] != null)
            {
                if (Session["DataBase"].ToString() == "DataBase")
                {
                    SetSessionDataBase("DataBase");
                    ViewBag.Checked = true;
                }
                else
                {
                    SetSessionDataBase("Session");
                    ViewBag.Checked = false;
                }
            }
            else
                Session["DataBase"] = "Session";
            return View();
        }

        [HttpPost]
        public ActionResult Index(ProductsListVirtualModel vM)
        {
            SetSessionDataBase(vM.dB);
            return View();
        }

        private void SetSessionDataBase(string db)
        {
            Session["DataBase"] = db;
        }
    }
}
