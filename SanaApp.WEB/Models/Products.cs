using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SanaApp.WEB.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please Enter Product Number"), Display(Name = "Product Number")]
        public string ProductNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Title"), Display(Name = "Product Title")]
        public string ProductTitle { get; set; }

        [Required(ErrorMessage = "Please Enter Price"), Display(Name = "Product Price"), Column(TypeName = "money")]
        public decimal ProductPrice { get; set; }
    }
}