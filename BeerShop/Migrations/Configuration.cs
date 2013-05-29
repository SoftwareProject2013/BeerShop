namespace BeerShop.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BeerShop.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<BeerShop.Models.BeerShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            //Update-Databse

        }

        protected override void Seed(BeerShop.Models.BeerShopContext context)
        {
            

            //var Orders = new List<Order>
            //    {
                   
            //        new Order { status = Order.pending, orderItems = new List<OrderItem>(), customer = (Customer) context.Users.Find(1004)}
            //    };
            //Orders.ForEach(s => context.Orders.Add(s));
            //context.SaveChanges();

        }
    }
}
