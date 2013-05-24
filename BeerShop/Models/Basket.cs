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

        public void AddOrderItem(OrderItem orderItem)
        {
            if (orderItem.item.isStillOnSale == false)
            {
                throw (new Exception("item is not for sale"));

            }
            if (orderItem.amount < 0)
            {
                throw (new Exception("canno't realise order with amount lower than 1"));
            }
            if (orderItem.amount > orderItem.item.stockCount)
            {
                throw (new Exception("not enought product on stock"));
            }
            
            orderItems.Add(orderItem);
        }

        public void clearBasket()
        {
            orderItems = new List<OrderItem>();
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