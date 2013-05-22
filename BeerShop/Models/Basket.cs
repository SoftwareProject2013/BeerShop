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

        public ICollection<OrderItem> orderItems { set; get; }

        public Basket() {
            this.orderItems =  new List<OrderItem>() { };
        }

        public double sum()
        {
            double s = 0;
            foreach(OrderItem item in orderItems)
            {
                s += item.sum(true);
            }
            return s;
        }
    }
}