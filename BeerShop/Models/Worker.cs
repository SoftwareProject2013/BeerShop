using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerShop.Models
{
    public class Worker : User
    {
        //moderatorPermission only can modify comments, manage items, categories
        //workerPermission can modify also orders (for example order status)
        //masterPermission can modify everything (orders, users, baskets)
        public static readonly int moderatorPermission = 1;
        public static readonly int workerPermission = 2;
        public static readonly int masterPermission = 3;

        //What is it? is required?
        public int permissions { get; set; }
    }
}