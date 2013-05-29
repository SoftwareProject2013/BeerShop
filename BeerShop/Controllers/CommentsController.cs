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
            if (itemHelper.comment.content != null && itemHelper.comment.content.Length < 1)
            {
                ModelState.AddModelError("", "Comment should have at least 1 letter");
            }
            Comment comment = itemHelper.comment;
            comment.item = db.Items.FirstOrDefault(i => i.ItemID == itemHelper.item.ItemID );
            comment.date = DateTime.UtcNow;
            comment.author = db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
            if (comment.content != null && comment.author != null &&  comment.item != null)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Items", new { id = comment.item.ItemID });
            }

            return RedirectToAction("Details", "Items", new { id = comment.item.ItemID, message = "Problem with comments it should have minimum 1 letter or you are not logged in" });
        }

        
        //
        // POST: /Comments/Edit/5

        [HttpPost]
        public ActionResult Edit(Comment commentNew)
        {
            Comment comment = db.Comments.FirstOrDefault(c => c.CommentID == commentNew.CommentID);
            comment.date = DateTime.UtcNow;
            comment.content = commentNew.content;
           
            if ((comment.content.Length > 0  && comment.author != null && comment.item != null && (User.Identity.Name == comment.author.email || User.IsInRole("Admin") )))
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Items", new { id = comment.item.ItemID });
            }
            return RedirectToAction("Details", "Items", new { id = comment.item.ItemID, message = "Problem with comments it should have minimum 1 letter or you are not logged in" });
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