using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace BeerShop.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        [RegularExpression(".+@.+")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        public string address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime birth { get; set; }

        public bool locked { get; set; }
    }

}