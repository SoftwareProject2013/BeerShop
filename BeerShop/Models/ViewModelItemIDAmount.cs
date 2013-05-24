using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerShop.Models
{
    public class ViewModelItemIDAmount
    {
        public ViewModelItemIDAmount()
        {
        }
        public ViewModelItemIDAmount(int itemID, int amount = 0)
        {
            this.itemID = itemID;
        }
        public int amount { set; get; }
        public int itemID { set; get; }
    }
}