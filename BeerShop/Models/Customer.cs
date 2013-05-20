using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BeerShop.Models
{
    public class Customer : User
    {
        //ID ?? CustomerID or UserID
        //[Required]
        public virtual Basket basket { get; set; }

        public virtual ICollection<Order> orders { get; set; }

    }
}
