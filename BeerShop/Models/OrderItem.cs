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

        public OrderItem() { }

        public OrderItem(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        // compute sum of the items
        // If actual = true, then compute the sum from actual item.price,
        // else from saved price
        public double sum(Boolean actual)
        {
            if (actual) return amount * item.Price;
            return amount*price;
        }
        public double sum()
        {
            return sum(false);
        }

        // will save actual price for the order
        public void savePrice()
        {
            price = item.Price;
        }

    }
}