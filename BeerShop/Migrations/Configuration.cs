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

                var Items = new List<Item>
                {
                    new Item { name = "Albani Odense", description = "blabla", stockCount = 5, Price = 2, isStillOnSale = true, categories = new List<CategoryItem>(){cI1, cI5}},
                    new Item { name = "Albani Odense Classic", description = "blabla", stockCount = 5, Price = 2, isStillOnSale = true, categories = new List<CategoryItem>(){cI2, cI4}}
                };
                Items.ForEach(s => context.Items.Add(s));
                context.SaveChanges();
            }

        }
    }
}
