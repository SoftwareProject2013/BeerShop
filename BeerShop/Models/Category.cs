using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BeerShop.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { set; get; }

        [Required]
        public string name { set; get; }

        public virtual ICollection<CategoryItem> categories { get; set; }
    }
}