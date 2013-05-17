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
    public class BasketsController : Controller
    {
        private BeerShopContext db = new BeerShopContext();

        //
        // GET: /Baskets/

        public ActionResult Index()
        {
            return View(db.Baskets.ToList());
        }

        //
        // GET: /Baskets/Details/5

        public ActionResult Details(int id = 0)
        {
            Basket basket = db.Baskets.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        //
        // GET: /Baskets/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Baskets/Create

        [HttpPost]
        public ActionResult Create(Basket basket)
        {
            if (ModelState.IsValid)
            {
                db.Baskets.Add(basket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(basket);
        }

        //
        // GET: /Baskets/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Basket basket = db.Baskets.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        //
        // POST: /Baskets/Edit/5

        [HttpPost]
        public ActionResult Edit(Basket basket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(basket);
        }

        //
        // GET: /Baskets/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Basket basket = db.Baskets.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        //
        // POST: /Baskets/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Basket basket = db.Baskets.Find(id);
            db.Baskets.Remove(basket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}