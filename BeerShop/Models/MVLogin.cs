using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BeerShop.Models
{
    public class MVLogin
    {
        public string email { set; get; }
        [DataType(DataType.Password)]
        public string password { set; get; }
    }
}