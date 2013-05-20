using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerShop.Models;

namespace BeerShop.Controllers
{
    public class UsersController : Controller
    {
        private BeerShopContext db = new BeerShopContext();

        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /Users/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            Customer customer = new Customer();
            return View(customer);


        }

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(Customer c)
        {

            //c.basket = db.Baskets.Find(1004);
            if (ModelState.IsValid)
            {
                db.Users.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            String messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                                           .Select(v => v.ErrorMessage + " " + v.Exception));
            Console.WriteLine(messages);

            return View(c);
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        public ActionResult EditOrder(int id = 0)
        {
            Customer user = (Customer)db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /Users/EditOrder/5

        [HttpPost]
        public ActionResult EditOrder(Customer user)
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
            return View(user);
        }

    }
}