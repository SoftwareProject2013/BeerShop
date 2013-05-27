using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace BeerShop.Models
{
    public abstract class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        [RegularExpression(".+@.+")]
        [Display(Name="Email address: ")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string password { get; set; }

        public string passwordSalt { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        [DataType(DataType.MultilineText)]
        public string address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime birth { get; set; }

        public bool locked { get; set; }

        public bool isAdult()
        {
            if (DateTime.Now.Year - this.birth.Year < 18)
                return false;
            else
                return true;
        }

    }

}