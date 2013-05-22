using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace BeerShop.Models
{
    public class ModelViewCategoryItem
    {
        public string selectedCategoryItem { set; get; }
        public CategoryItem categoryItem { set; get; }
    }
}