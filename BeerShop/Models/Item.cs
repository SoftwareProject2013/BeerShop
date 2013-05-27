using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace BeerShop.Models
{
    public class Item
    {
        [Key]
        public int ItemID { set; get; }

        [Required]
        public string name { set; get; }

        public string imageURL { set; get; }

        [DataType(DataType.MultilineText)]
        public string description { set; get; }

        [Range(0, int.MaxValue)]
        public int stockCount { set; get; }

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public virtual ICollection<CategoryItem> categories { get; set; }
        public virtual ICollection<Comment> comments { get; set; }

        [Required]
        public Boolean isStillOnSale { set; get; }

    
    }

}
