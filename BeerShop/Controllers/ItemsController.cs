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
    public class ItemsController : Controller
    {
        private BeerShopContext db = new BeerShopContext();

        //
        // GET: /Items/

        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }

        //
        // GET: /Items/Details/5

        public ActionResult Details(int id = 0)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        //
        // GET: /Items/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Items/Create

        [HttpPost]
        public ActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        //
        // GET: /Items/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        //
        // POST: /Items/Edit/5

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        //
        // GET: /Items/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        //
        // POST: /Items/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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