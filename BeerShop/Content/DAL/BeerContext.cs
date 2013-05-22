using System;
using System.Collections.Generic;
using System.Data.Entity;
using BeerShop.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Beershop.Models
{
    public class BeerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}