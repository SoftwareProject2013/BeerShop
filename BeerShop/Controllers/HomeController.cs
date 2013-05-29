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
            dbInit();
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
                ViewBag.SpecialOffer.name = "no beer";
                ViewBag.SpecialOffer.description = "no beer";
            }

            Random random2 = new Random();
            int randomNumber2 = random.Next(0, li);
            
            try
            {
                ViewBag.BeerMonth = list[1];
            }
            catch
            {
                ViewBag.BeerMonth = new Item();
                ViewBag.BeerMonth.name = "no beer";
                ViewBag.beerMonth.description = "no beer";
            }
            return View();
        }

        public void dbInit()
        {
            if (db.Users.ToList().Count <1 )
            {
                Category c1 = new Category { name = "by Country" };
                Category c2 = new Category { name = "by Type" };

                var Categories = new List<Category> { c1, c2 };
                Categories.ForEach(s => db.Categories.Add(s));
                db.SaveChanges();

                CategoryItem cI1 = new CategoryItem { name = "Polish", category = c1 };
                CategoryItem cI2 = new CategoryItem { name = "Spanish", category = c1 };
                CategoryItem cI3 = new CategoryItem { name = "Lager", category = c2 };
                CategoryItem cI4 = new CategoryItem { name = "Black beer", category = c2 };
                CategoryItem cI5 = new CategoryItem { name = "Pilsner", category = c2 };

                var CategoryItems = new List<CategoryItem> { cI1, cI2, cI3, cI4, cI5 };
                CategoryItems.ForEach(s => db.CategoryItems.Add(s));
                db.SaveChanges();

                Item i1 = new Item { name = "Albani Odense", 
                    imageURL= "http://www.stiki.dk/typo3temp/pics/b039662816.jpg"
                    , description = "Odense Classic is a pilsener, though it has a more dark colour than ordinary beers of the same type. The beer has a more rounded, but still powerful taste of malt and hops. It was introduced at the brewery's 140th anniversary in 1999."

                , stockCount = 400, Price = 1, isStillOnSale = true, categories = new List<CategoryItem>() { cI1, cI5 } };
                Item i2 = new Item { name = "Albani Odense Giraff", description = "Giraf Beer is a strong pilsener. It was first brewed in 1962, when Odense Zoo's giraffe (Danish: giraf) Kalle was found dead, as the Albani Breweries had previously used this giraffe in its advertisement, it decided to create a special beer, the profits of which would be spent on purchasing a new giraffe for the zoo. The first year's production raised enough funds to buy two giraffes for the zoo. Alcohol by volume: 7.2%"
                ,
                                     stockCount = 200,
                                     Price = 1,
                                     imageURL = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSuaBhK31plcmii5FH50cE68LVo5_85YKOdOQwkfGxYAmopf5p620rLsOWm",
                                     isStillOnSale = true,
                                     categories = new List<CategoryItem>() { cI2, cI4 }
                };

                var Items = new List<Item> { i1, i2 };
                Items.ForEach(s => db.Items.Add(s));
                db.SaveChanges();

                OrderItem oI1 = new OrderItem { item = i1, amount = 3 };

                var OrderItems = new List<OrderItem> { oI1 };
                OrderItems.ForEach(s => db.OrderItems.Add(s));
                db.SaveChanges();

                Basket b1 = new Basket { orderItems = new List<OrderItem>() { } };
                b1.orderItems.Add(oI1);
                var Baskets = new List<Basket> { b1 };
                Baskets.ForEach(s => db.Baskets.Add(s));
                db.SaveChanges();

                var Users = new List<User>
                {
                    new Customer { firstName = "Peter", lastName = "Languila", email = "peterlanguila@gmail.com", password = "holahola", birth = new DateTime(1990,07,16), basket = b1 },
                    new Worker { firstName = "Manu", lastName = "Chao", email = "manuchao@gmail.com", password = "holahola", birth = new DateTime(1980,07,16), permissions = 3 }
                };
                Users.ForEach(s => db.Users.Add(s));
                db.SaveChanges();

                {
                    Worker w = new Worker();
                    w.firstName = "Admin";
                    w.lastName = "Admin";
                    w.address = "Admins";
                    w.birth = new DateTime(1990, 12, 12);
                    w.email = "admin@gmail.com";
                    w.password = "admin1";
                    w.locked = false;
                    w.permissions = 3;
                    w.phone = "222333444";
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encryptPass = crypto.Compute(w.password);
                    w.password = encryptPass;
                    w.passwordSalt = crypto.Salt;
                    db.Users.Add(w);
                    db.SaveChanges();
                }
            }
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
