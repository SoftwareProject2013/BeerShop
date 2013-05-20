using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BeerShop.Models
{
    public class BeerShopContext : DbContext
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }  

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}