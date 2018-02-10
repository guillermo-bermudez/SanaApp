using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanaApp.WEB.VirtualModel
{
    public class ProductsListVirtualModel
    {
        public List<ProductsVirtualModel> Products { get; set; }
        public string dB { get; set; }
    }
}