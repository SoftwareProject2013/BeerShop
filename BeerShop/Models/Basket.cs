using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BeerShop.Models
{
    public class Basket
    {
        [Key]
        public int BasketID { set; get; }

        [Required]
        public virtual Dictionary<Item, int> items { set; get; } //Item + amountOfItems
    }
}