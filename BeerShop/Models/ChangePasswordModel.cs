using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BeerShop.Models
{
    public class ChangePasswordModel
    {   
        [Required]
        [DataType(DataType.Password)]
        [MinLength(4) ]
        public string password { get; set; }
        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string passwordRetype { get; set; }
        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string oldPassword { get; set; }
    }
}