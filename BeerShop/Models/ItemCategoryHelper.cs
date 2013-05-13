using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;




namespace BeerShop.Models
{
    public class ItemCategoryHelper
    {
      public  Item item { set; get; }

      public string SelectedCountry { set; get; }
      public string SelectedType { set; get; }
      public Comment comment { set; get; }
    }
}