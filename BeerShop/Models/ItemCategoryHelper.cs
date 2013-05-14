using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;




namespace BeerShop.Models
{
    public class ItemCategoryHelper
    {
        public ItemCategoryHelper()
        {
            categoryTypeCategoryDictionary = new Dictionary<string, string>();
        }
      public  Item item { set; get; }
      public Dictionary<string, string> categoryTypeCategoryDictionary { set; get; }
      public string localHelper { set; get; }
      public Comment comment { set; get; }
      public string TextShort { get { return item.description.Substring(0, 30) + " ... "; } }
    }
}