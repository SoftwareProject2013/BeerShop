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
        // GET: /Users/CreateCustomer

        public ActionResult CreateCustomer()
        {
            Customer c = new Customer();
            //add session basket
            return View(c);
        }

        //
        // POST: /Users/CreateCustomer
        //user na customera, user jest abstrakcyjna klasą 
        [HttpPost]
        public ActionResult CreateCustomer(Customer customer)
        {
            //asign session basket
            customer.basket = db.Baskets.Find(1);
            if (!ModelState.IsValid)
            {
                db.Users.Add((Customer)customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            String messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
.Select(v => v.ErrorMessage + " " + v.Exception));
            return View(customer);
        }

        //
        // GET: /Users/EditCustomer/5

        public ActionResult EditCustomer(int id = 0)
        {
            Customer user = (Customer) db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/EditCustomer/5

        [HttpPost]
        public ActionResult EditCustomer(Customer user)
        {
            user.basket = db.Baskets.Find(user.basket.BasketID);
            if (ModelState.IsValid)
            {
                db.Entry((Customer)user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            String messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
.Select(v => v.ErrorMessage + " " + v.Exception));
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
