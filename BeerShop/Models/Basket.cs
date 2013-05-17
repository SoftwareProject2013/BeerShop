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
        public virtual ICollection<OrderItem> orderItems { set; get; } //Item + amountOfItems. we dont need to save the price parameter in the Basket
    }
}