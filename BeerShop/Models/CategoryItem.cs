using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BeerShop.Models
{
    public class CategoryItem
    {
        [Key]
        public int CategoryItemID { set; get; }

        [Required]
        public string name { set; get; }
    }
}
