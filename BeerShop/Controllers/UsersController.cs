using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerShop.Models;
using System.Web.Security;
using System.Security.Principal;

namespace BeerShop.Controllers
{
    public class UsersController : Controller
    {
        private BeerShopContext db = new BeerShopContext();

        //
        // GET: /Users/
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /Users/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null )
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Users/CreateCustomer

        public ActionResult CreateCustomer()
        {
            
            Customer c = new Customer();
            return View(c);
        }

        // POST: /Users/CreateCustomer
        [HttpPost]
        public ActionResult CreateCustomer(Customer c)
        {
            if (!c.isAdult())
            {
                ModelState.AddModelError("", "Sorry! You have to be over 18");
            }
            else
            {
                Basket b = new Basket();
                c.basket = b;
                db.Baskets.Add(b);
                db.SaveChanges();
                if (db.Users.FirstOrDefault(u => u.email == c.email) != null)
                {
                    return View(c);
                }
                if (ModelState.IsValid)
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encryptPass = crypto.Compute(c.password);
                    c.password = encryptPass;
                    c.passwordSalt = crypto.Salt;
                    db.Users.Add(c);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    db.Baskets.Remove(b);
                    db.SaveChanges();
                }
                //var errors = ModelState.Values.SelectMany(v => v.Errors);
                //String messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage + " " + v.Exception));
            }
            return View(c);
        }


        //
        // GET: /Users/EditCustomer/5
        [Authorize(Roles = "Customer")]
        public ActionResult EditCustomer(int id = 0)
        {
            Customer user = (Customer)(from u in db.Users
                                  where u.email == User.Identity.Name
                                  select u).First();
            if (user == null)
            {
                return HttpNotFound();
            }
            TempData["Something"] = user.basket;
            return View(user);
        }

        //
        // POST: /Users/EditCustomer/5

        [HttpPost]
        [Authorize(Roles = "Customer, Admin")]
        public ActionResult EditCustomer(Customer user)
        {
            user.basket = (Basket)TempData["Something"];
            TempData["Something"] = user.basket;

            if (ModelState.IsValid)
            {
                db.Entry((Customer)user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        //
        // GET: /Users/Lock/5
        [Authorize(Roles="Admin")]
        public ActionResult Lock(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (user.locked == true)
            {
                user.locked = false;
            }
            else
            {
                user.locked = true;
            }

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        //
        // GET: /Users/EditOrder/5

        [Authorize(Roles = "Customer")]
        public ActionResult EditOrder(int id = 0)
        {
            Customer user = (Customer)db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /Users/EditOrder/5

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult EditOrder(Customer user)
        {
            if (user.address != null)
            {
                user.basket = db.Baskets.Find(user.basket.BasketID);
                var query = from o in db.Orders
                            where o.customer.UserID == user.UserID
                            select o;

                foreach (var item in query)
                {
                    user.orders.Add(item);
                }
                if (!ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    var query2 = from o in db.Orders
                                 where o.customer.UserID == user.UserID
                                 select o;
                    return RedirectToAction("DetailsOrderItems", "Orders", new { id = query2.OrderByDescending(item => item.OrderID).First().OrderID });
                }
                String messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                                               .Select(v => v.ErrorMessage + " " + v.Exception));
            }
            ModelState.AddModelError("", "Sorry! You address is not valid.");
            return View(user);
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(BeerShop.Models.Customer customer)
        {
            if (customer.email != null && customer.password != null)
            {
                User user= isValid(customer.email, customer.password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(customer.email, false);
                    string roles = "";
                    if (user is Customer)
                    {
                        roles += "Customer";
                    }
                    else
                    {
                        roles += "Admin";
                    }
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                      1,
                      customer.email,
                      DateTime.Now,
                      DateTime.Now.AddMinutes(20),  
                      false,
                      roles,
                      "/");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                       FormsAuthentication.Encrypt(authTicket));
                    Response.Cookies.Add(cookie);
                   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect");
                }
            }
            return View();
        }

        [Authorize(Roles = "Customer, Admin")]
        public ActionResult LogOut()
        {

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket =
                                      FormsAuthentication.Decrypt(authCookie.Value);
                authCookie.Value = null;
                authCookie.Expires = DateTime.Now;
            }
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Index","Home");
        }
        
        private User isValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            
            var user = db.Users.FirstOrDefault(u => u.email == email);
            if (user != null)
            {
                //if (user.password == crypto.Compute(password, user.passwordSalt))
                //{
                    return user;
                //}
            }
            return null;
        }

    }


}
