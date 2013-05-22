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
    public class CategoryItemsController : Controller
    {
        private BeerShopContext db = new BeerShopContext();

        //
        // GET: /CategoryItems/

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
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
            var categoriesList = db.Categories;
            SelectList SelecteCategoryItemList = new SelectList(categoriesList, "CategoryId", "name");
            ViewBag.selectCategoryItemList = SelecteCategoryItemList;
            return View();
        }

        //
        // POST: /CategoryItems/Create

        [HttpPost]
        public ActionResult Create(ModelViewCategoryItem MVcategoryItem)
        {
            int categoryID = int.Parse(MVcategoryItem.selectedCategoryItem);
            Category category = db.Categories.FirstOrDefault(c => c.CategoryID == categoryID);
            MVcategoryItem.categoryItem.category = category;
  
            if (category != null && MVcategoryItem.categoryItem != null)
            {
                db.CategoryItems.Add(MVcategoryItem.categoryItem);
                category.categories.Add(MVcategoryItem.categoryItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(MVcategoryItem);
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
            
            ModelViewCategoryItem MVCategoryItem = new ModelViewCategoryItem();
            MVCategoryItem.categoryItem = categoryitem;
            var categoriesList = db.Categories.ToList();
            SelectList SelecteCategoryItemList = new SelectList(categoriesList, "CategoryId", "name", categoryitem.category.CategoryID.ToString());
            ViewBag.selectCategoryItemList = SelecteCategoryItemList;
            return View(MVCategoryItem);
        }

        //
        // POST: /CategoryItems/Edit/5

        [HttpPost]
        public ActionResult Edit(ModelViewCategoryItem MVcategoryItem)
        {
            int categoryID = int.Parse(MVcategoryItem.selectedCategoryItem);
            Category category = db.Categories.FirstOrDefault(c => c.CategoryID == categoryID);
            MVcategoryItem.categoryItem.category = category;

            if (category != null && MVcategoryItem.categoryItem != null)
            {
                db.CategoryItems.Add(MVcategoryItem.categoryItem);
                category.categories.Add(MVcategoryItem.categoryItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(MVcategoryItem);
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