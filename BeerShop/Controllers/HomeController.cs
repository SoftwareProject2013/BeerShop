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
            if (db.Users.FirstOrDefault(u => u.email == "Admin@gmail.com") == null)
            {
                Worker admin = new Worker();
                admin.address = "1";
                admin.birth = new DateTime(1950, 1, 1);
                admin.email = "Admin@gmail.com";
                admin.firstName = "Admin";
                admin.lastName = "Admin";
                admin.locked = false;
                admin.permissions = 2;
                admin.password = "admin1";
                var crypto = new SimpleCrypto.PBKDF2();
                var encryptPass = crypto.Compute(admin.password);
                admin.password = encryptPass;
                admin.passwordSalt = crypto.Salt;
               

                db.Users.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", new { message = "Admin created email: Admin@gmail.com password: admin1" });
            }
            ViewBag.Message = message;
            List<Item> list = db.Items.ToList();
            int li = list.Count;
            Random random = new Random();
            int randomNumber = random.Next(0,li-1 );
            ViewBag.SpecialOffer = list[randomNumber];
            return View(db.Items.Take(1));
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
