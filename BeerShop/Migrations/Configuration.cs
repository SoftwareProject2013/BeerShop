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

        }

        protected override void Seed(BeerShop.Models.BeerShopContext context)
        {
            //Basket b1 = new Basket { orderItems = new List<OrderItem>() { } };
            //context.Baskets.Add(b1);
            //context.SaveChanges();

            if (context.Categories.Find(1) == null)
            {
                Category c1 = new Category { name = "by Country" };
                Category c2 = new Category { name = "by Type" };

                var Categories = new List<Category> { c1, c2 };
                Categories.ForEach(s => context.Categories.Add(s));
                context.SaveChanges();

                CategoryItem cI1 = new CategoryItem { name = "Polish", category = c1 };
                CategoryItem cI2 = new CategoryItem { name = "Spanish", category = c1 };
                CategoryItem cI3 = new CategoryItem { name = "Lager", category = c2 };
                CategoryItem cI4 = new CategoryItem { name = "Black beer", category = c2 };
                CategoryItem cI5 = new CategoryItem { name = "Pilsner", category = c2 };

                var CategoryItems = new List<CategoryItem> { cI1, cI2, cI3, cI4, cI5 };
                CategoryItems.ForEach(s => context.CategoryItems.Add(s));
                context.SaveChanges();

                Item i1 = new Item { name = "Albani Odense", description = "blabla", stockCount = 5, Price = 2, isStillOnSale = true, categories = new List<CategoryItem>() { cI1, cI5 } };
                Item i2 = new Item { name = "Albani Odense Classic", description = "blabla", stockCount = 5, Price = 2, isStillOnSale = true, categories = new List<CategoryItem>() { cI2, cI4 } };
                var Items = new List<Item> { i1, i2 };
                Items.ForEach(s => context.Items.Add(s));
                context.SaveChanges();

                OrderItem oI1 = new OrderItem { item = i1, amount = 3 };
                var OrderItems = new List<OrderItem> { oI1 };
                OrderItems.ForEach(s => context.OrderItems.Add(s));
                context.SaveChanges();
                Basket b1 = new Basket { orderItems = new List<OrderItem>() { } };
                b1.orderItems.Add(oI1);
                var Baskets = new List<Basket> { b1 };
                Baskets.ForEach(s => context.Baskets.Add(s));
                context.SaveChanges();
            }

        }
    }
}
