using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BeerShop.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { set; get; }

        [Required]
        [DataType(DataType.Text)]
        public string content { set; get; }

        //[Required]
        public virtual User author { set; get; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }

        [Required]
        public virtual Item item { set; get; }


    }
}
