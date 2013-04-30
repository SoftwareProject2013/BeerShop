using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BeerShop.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { set; get; }

        [Required]
        public virtual Item item { get; set; }

        public double price { get; set; }

        [Required]
        public int amount { get; set; }
    }
}