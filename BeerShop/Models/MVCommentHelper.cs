using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerShop.Models
{
    public class MVCommentHelper
    {
        public MVCommentHelper(IEnumerable<Comment> commentsList, int selectedComment)
        {
            this.commentsList = commentsList;
            this.selectedComment = selectedComment;
        }
        public IEnumerable<Comment> commentsList { get; set; }
        public int selectedComment { get; set; }
    }
}