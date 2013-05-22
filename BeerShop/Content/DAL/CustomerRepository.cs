using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BeerShop.Models;
using BeerShop.DAL;

namespace Beershop.DAL
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private BeerContext context;

        public CustomerRepository(BeerShopContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }

        public Customer GetCustomerByID(int UserID)
        {
            return context.Customers.Find(UserID);
        }

        public void InsertStudent(Customer customer)
        {
            context.Customers.Add(customer);
        }

        public void DeleteStudent(int UserID)
        {
            Customer customer = context.Customers.Find(UserID);
            context.Customer.Remove(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            context.Entry(customer).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}