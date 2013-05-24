﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerShop.Models;

namespace BeerShop.Controllers
{
    public class OrdersController : Controller
    {
        private BeerShopContext db = new BeerShopContext();

        //
        // GET: /Orders/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //return View(db.Orders.ToList().Where (item => item.status  == Order.processing));
            return View(db.Orders.ToList());
        }

        //
        // GET: /Orders/Details/5

        public ActionResult Details(int id = 0)
        {
            Order order;
            if(id != 0)
                order = db.Orders.Find(id);
            else
            {
                order = db.Orders.FirstOrDefault(o => o.customer.email == User.Identity.Name);
            }
            if (order == null)
            {
                return View();
            }
            double totalPrice = 0;
            foreach (OrderItem oI in order.orderItems)
            {
                totalPrice += oI.price;
            }
            ViewBag.totalPrice = totalPrice;
            
            return View(order);
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            Order order = new Order();
            order.createdDate = DateTime.UtcNow;

            //modify later, right now are set like a null (maxValue for DateTime type)
            order.dispachedDate = DateTime.MaxValue;
            order.deliveredDate = DateTime.MaxValue;
            return Create(order);
        }

        //
        // POST: /Orders/Create

        [HttpPost]
        public ActionResult Create(Order order)
        {
            //Change status, createDate and adding the current price of the item to the OrderItem list 
            order.status = Order.pending;
               

            //modify orderItem list
            order.orderItems = new List<OrderItem>();
            order.orderItems.Add(db.OrderItems.Find(2));//order.orderItems.Add(db.Baskets.Find(2).orderItems.ElementAt<OrderItem>(2));

            //modify user
            order.customer = (Customer)db.Users.Find(1);

            //Add prices
            foreach (OrderItem oI in order.orderItems)
            {
                oI.price = oI.item.Price;
            }
            
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                //db.Entry(order.orderItems.First<OrderItem>()).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditOrder", "Users", new { id = order.customer.UserID });
            }

            //modify basket
            return RedirectToAction("Edit", "Baskets", new { id = db.Baskets.Find(2).BasketID });
        }

        //
        // GET: /Orders/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //get current order
            //SelectList sl = new SelectList(new List<string>() { "pending", "processing", "dispached", "delivered", "canceled" });
            //ViewBag.SelectList = sl;

            Order order = db.Orders.Find(id);
            TempData["Something"] = order.orderItems;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            order.customer = (Customer) db.Users.Find(order.customer.UserID);
            order.orderItems = (ICollection<OrderItem>) TempData["Something"];
            TempData["Something"] = order.orderItems;
            if (!ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            String messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(v => v.ErrorMessage + " " + v.Exception));
            return View(order);
        }

        //
        // GET: /Orders/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //delete orderitem and basket?
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //
        // GET: /Orders/DetailsOrderItems/5

        public ActionResult DetailsOrderItems(int id = 0)
        {
            //get current order
            Order order = db.Orders.Find(id);
            //calculate total price
            double totalPrice = 0;
            foreach (OrderItem oI in order.orderItems)
            {
                totalPrice += oI.price;
            }
            ViewBag.totalPrice = totalPrice;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // GET: /Orders/Confirmation/5

        public ActionResult Confirmation(int id = 0)
        {
            //get current order
            Order order = db.Orders.Find(id);
            //calculate total price
            double totalPrice = 0;
            foreach (OrderItem oI in order.orderItems)
            {
                totalPrice += oI.price;
            }
            ViewBag.totalPrice = totalPrice;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult Finish(int id = 0)
        {
            //get current order
            Order order = db.Orders.Find(id);
            //calculate total price
            foreach (var item in order.orderItems)
            {
                int subtract = item.amount;
                item.item.stockCount = item.item.stockCount - subtract;
            }
            order.customer.basket.orderItems = new List<OrderItem>();
            order.status = Order.processing;
            order.createdDate = DateTime.UtcNow;
            
            if (order == null)
            {
                return HttpNotFound();
            }
            return Index();
        }

        public ActionResult bootstrap()
        {
            return RedirectToAction("Index", "Home");
        }

    }
}