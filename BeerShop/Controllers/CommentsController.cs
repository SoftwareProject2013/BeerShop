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
    public class CommentsController : Controller
    {
        private BeerShopContext db = new BeerShopContext();

        //
        // GET: /Comments/

        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        //
        // GET: /Comments/Details/5

        public ActionResult Details(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // GET: /Comments/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Comments/Create

        [HttpPost]
        public ActionResult Create(ItemCategoryHelper itemHelper)
        {
            
            Comment comment = itemHelper.comment;
            comment.item = db.Items.FirstOrDefault(i => i.ItemID == itemHelper.item.ItemID );
            comment.date = DateTime.UtcNow;
            comment.author = null;
            if (comment.content.Length >0 /* && comment.author != null*/ &&  comment.item != null)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Items", new { id = comment.item.ItemID });
            }

            return RedirectToAction("Details", "Items", new { id = comment.item.ItemID });
        }

        //
        // GET: /Comments/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comments/Edit/5

        [HttpPost]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        //
        // GET: /Comments/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comments/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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