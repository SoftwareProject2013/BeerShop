using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BeerShop.Models
{
    public enum Status { pending, processing, dispached, delivered, canceled} 


    public class Order
    {
        public static readonly int pending = 1;
        public static readonly int processing = 2;
        public static readonly int dispached = 3;
        public static readonly int delivered = 4;
        public static readonly int canceled = 5;

        [Key]
        public int OrderID { set; get; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime createdDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime dispachedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime deliveredDate { get; set; }

        //[Required]
        public virtual ICollection<OrderItem> orderItems { set; get; } //Item + amountOfItems + priceOfItemInTheMomentOfTheOrder

        [Required]
        public int status { get; set; }

        [Required]
        public virtual Customer customer { set; get; }

    }
}