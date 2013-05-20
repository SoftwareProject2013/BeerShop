using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerShop.Models;

namespace BeerShop.DAL
{
    public interface ICustomerRepository : IDisposable
    {
        IEnumerable <Customer> GetStudents();
        Customer GetCustomerByID(int UserId);
        void InsertCustomer(Customer customer);
        void DeleteCustomer(int UserID);
        void UpdateCustomer(Customer customer);
        void Save();
    }
}