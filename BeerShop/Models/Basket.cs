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

        public virtual ICollection<OrderItem> orderItems { set; get; }

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
            
                OrderItem oI = orderItems.FirstOrDefault(o => o.item == orderItem.item);
                if (oI != null)
                {
                    if ((oI.amount + orderItem.amount) > orderItem.item.stockCount)
                    {
                        throw (new Exception("not enought beer on stock try smaller amount"));
                    }
                    oI.amount += orderItem.amount;
                }
                else
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