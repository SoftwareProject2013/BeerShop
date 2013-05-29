using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BeerShop.Models;


namespace BeerShop.Controllers
{
    public class HomeController : Controller
    {
        BeerShopContext db = new BeerShopContext();
        public ActionResult Index(string message="")
        {
         
            ViewBag.Message = message;
            List<Item> list = db.Items.ToList();
            int li = list.Count;
            Random random = new Random();
            int randomNumber = random.Next(0, li);
            try
            {
                ViewBag.SpecialOffer = list[randomNumber];
            }
            catch 
            {
                ViewBag.SpecialOffer = new Item ();
            }

            Random random2 = new Random();
            int randomNumber2 = random.Next(0, li);
            while (randomNumber == randomNumber2)
            {
                random2 = new Random();
                randomNumber2 = random.Next(0, li);
            }
            try
            {
                ViewBag.BeerMonth = list[3];
            }
            catch
            {
                ViewBag.SpecialOffer = new Item();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
