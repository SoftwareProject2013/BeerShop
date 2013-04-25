using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerShop.Models
{
    public class Worker : User
    {
        enum Permission { /*¿?¿?¿?¿?¿?*/ } //Any more?

        //ID ?? CustomerID or UserID
        //What is it? is required?
        public string roomNo { get; set; }


        //What is it? is required?
        public Permission permissions { get; set; }
    }
}