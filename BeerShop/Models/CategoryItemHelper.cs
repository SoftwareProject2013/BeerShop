using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BeerShop.Models
{
    public class CategoryItemHelper
    {

        [Required]
        public string name { set; get; }

        [Required]
        public virtual CategoryItem categoryitem { set; get; }
   
        public Item item { set; get; }

        public int SelectedCategory { set; get; }
    }
}
