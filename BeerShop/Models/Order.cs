using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BeerShop.Models
{
    public class Order
    {
        enum Status {pending, processing, dispached, delivered} //Any more?

        [Key]
        public int OrderID { set; get; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime createdDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime dispachedDate { get; set; }

        [Required]
        public virtual Dictionary<Item, Tuple<int, double>> items { set; get; } //Item + amountOfItems + priceOfItemInTheMomentOfTheOrder

        [Required]
        public Status status { get; set; }

        [Required]
        public virtual Customer customer { set; get; }

    }
}