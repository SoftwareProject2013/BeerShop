using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerShop.Models;
using System.Data.SqlClient;

namespace BeerShop.Controllers
{
    public class CategoryItemsController : Controller
    {
        private BeerShopContext db = new BeerShopContext();

        //
        // GET: /CategoryItems/

        public ActionResult Index()
        {
            return View(db.CategoryItems.ToList());
        }

        //
        // GET: /CategoryItems/Details/5

        public ActionResult Details(int id = 0)
        {
            CategoryItem categoryitem = db.CategoryItems.Find(id);
            if (categoryitem == null)
            {
                return HttpNotFound();
            }
            return View(categoryitem);
        }

        //
        // GET: /CategoryItems/Create

        public ActionResult Create()
        {
            //Database.SetInitializer<BeerShopContext>(null);
            var categoryList = (from q in db.Categories select new { C= q.CategoryID , n = q.name }).ToList() ;
            
            SelectList SelectCategoryList = new SelectList( categoryList, "C", "n");

            ViewBag.categoriesList = SelectCategoryList;
            return View();
        }

        //
        // POST: /CategoryItems/Create

        [HttpPost]
        public ActionResult Create(CategoryItemHelper categoryitem)
        {
            if (ModelState.IsValid)
            {
                db.CategoryItems.Add(categoryitem.categoryitem);
                //int categoryID = int.Parse(categoryitem.SelectedCategory);
               // db.Categories.FirstOrDefault(c => c.name == categoryitem.SelectedCategory).categories.Add(categoryitem.categoryitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryitem);
        }

        //
        // GET: /CategoryItems/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CategoryItem categoryitem = db.CategoryItems.Find(id);
            if (categoryitem == null)
            {
                return HttpNotFound();
            }
            return View(categoryitem);
        }

        //
        // POST: /CategoryItems/Edit/5

        [HttpPost]
        public ActionResult Edit(CategoryItem categoryitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryitem);
        }

        //
        // GET: /CategoryItems/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CategoryItem categoryitem = db.CategoryItems.Find(id);
            if (categoryitem == null)
            {
                return HttpNotFound();
            }
            return View(categoryitem);
        }

        //
        // POST: /CategoryItems/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryItem categoryitem = db.CategoryItems.Find(id);
            db.CategoryItems.Remove(categoryitem);
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