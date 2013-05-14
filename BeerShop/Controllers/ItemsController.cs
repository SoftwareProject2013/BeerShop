using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerShop.Models;
using PagedList;
using BeerShop.Models;

namespace BeerShop.Controllers
{
    public class ItemsController : Controller
    {
        private BeerShopContext db = new BeerShopContext();

        //
        // GET: /Items/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page,string categoryType,string category ,bool? clearDictionary )
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.PriceSort = sortOrder == "Price" ? "Price desc" : "Price" ;
            //ViewBag.beerCountry = beerCountry;
            //ViewBag.beerType = beerType;

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;
            var items = db.Items.ToList().AsQueryable();



            Dictionary<string, List<string>> categoryDictionary;
            categoryDictionary = Session["categoryFilter"] as Dictionary<string, List<string>>;
            if (categoryDictionary == null || (clearDictionary?? false) == true)
            {
                categoryDictionary = new Dictionary<string, List<string>>();
            }

                if (categoryType != null)
                {
                    if (!categoryDictionary.ContainsKey(categoryType))
                    {
                        List<string> categoryList = new List<string>();
                        categoryList.Add(category);
                        categoryDictionary.Add(categoryType, categoryList);
                    }
                    else
                    {
                        if (!categoryDictionary.FirstOrDefault(c => c.Key == categoryType).Value.Contains(category))
                        {
                            categoryDictionary.FirstOrDefault(c => c.Key == categoryType).Value.Add(category);
                        }
                        else
                        {
                            categoryDictionary.FirstOrDefault(c => c.Key == categoryType).Value.Remove(category);
                            if (categoryDictionary.FirstOrDefault(c => c.Key == categoryType).Value.Count == 0)
                            {
                                categoryDictionary.Remove(categoryType);
                            }
                        }
                    }
                    Session["categoryFilter"] = categoryDictionary;
                }
            Session["categoryFilter"] = categoryDictionary;
            
            
            if (categoryDictionary.Count > 0)
            {
                String SQLQuerry = "";
                
                    foreach (var categoryList in categoryDictionary.Values)
                    {
                        SQLQuerry += "(Select * FROM dbo.item ";

                        SQLQuerry += "Where itemID  in (Select Item_ItemID from dbo.CategoryItemItem where CategoryItem_CategoryItemID in (Select CategoryItemID from dbo.CategoryItem Where name in (";
                        for (int i = 0; i < categoryList.Count - 1; i++)
                        {
                            SQLQuerry += "'" + categoryList[i] + "',";
                        }
                        SQLQuerry += "'" + categoryList[categoryList.Count - 1] + "')))) Intersect ";

                    }
                    SQLQuerry = SQLQuerry.Substring(0,SQLQuerry.Length - 10) + ";";
                    var query = db.Items.SqlQuery(SQLQuerry).ToList();
                    items = query.AsQueryable();
                
            }
            

           
            if (!String.IsNullOrEmpty(searchString))
            {
               items = items.Where(s => s.name.ToUpper().Contains(searchString.ToUpper()) );
            }

            switch (sortOrder)
            {
                case  "Name desc":
                    items = items.OrderByDescending(i => i.name);
                    break;
                case "Price":
                    items= items.OrderByDescending(i => i.Price);
                    break;
                case "Price desc" :
                    items= items.OrderBy(i => i.Price);
                    break;
                default :
                    items = items.OrderBy(i => i.name);
                    break;
            }


            int PageSize = 3;
            int pagenumber = (page ?? 1);

            ViewBag.PermissionLevel = Worker.masterPermission;


            var categories = from c in db.Categories select c;

            
            return View(items.ToPagedList(pagenumber,PageSize));

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
            ViewBag.PermissionLevel = 3; // TODO permission level from user
            ViewBag.userLogged = true; // TODO check if user logged
            ItemCategoryHelper itemHelper = new ItemCategoryHelper();
            itemHelper.item = item;
            itemHelper.SelectedCountry = item.categories.FirstOrDefault(c => c.category.name.Equals("by Country")).name.ToString();
            itemHelper.SelectedType = item.categories.FirstOrDefault(c => c.category.name.Equals("by Type")).name.ToString();
            return View(itemHelper);
        }

        //
        // GET: /Items/Create

        public ActionResult Create()
        {

           // Where(c => c.category.name.Equals("by Country")).
            
            var countryList = db.CategoryItems.Where(c => c.category.name.Equals("by Country"));
            SelectList SelectCategoryList = new SelectList(countryList, "CategoryItemID", "name");

            ViewBag.countriesList = SelectCategoryList;

            var typesList = db.CategoryItems.Where(c => c.category.name.Equals("by Type"));
            SelectList SelectTypeList = new SelectList(typesList, "CategoryItemID", "name");
            ViewBag.typesList = SelectTypeList;
            return View();
        }

        //
        // POST: /Items/Create

        [HttpPost]
        public ActionResult Create(ItemCategoryHelper itemHelper)
        {
            if (ModelState.IsValid)
            {
                itemHelper.item.isStillOnSale = true;
                db.Items.Add(itemHelper.item);
                int countryID = int.Parse(itemHelper.SelectedCountry);
                int typeID = int.Parse(itemHelper.SelectedType);
                db.CategoryItems.FirstOrDefault(c => c.CategoryItemID == countryID).items.Add(itemHelper.item);
                db.CategoryItems.FirstOrDefault(c => c.CategoryItemID == typeID).items.Add(itemHelper.item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itemHelper.item);
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
            ItemCategoryHelper itemHelper = new ItemCategoryHelper();
            itemHelper.item = item;
            var selectedCountry = item.categories.FirstOrDefault(c => c.category.name.Equals("by Country"));
            var selectedType = item.categories.FirstOrDefault(c => c.category.name.Equals("by Type"));

            var countryList = db.CategoryItems.Where(c => c.category.name.Equals("by Country"));
            SelectList SelectCategoryList = new SelectList(countryList, "CategoryItemID", "name",selectedCountry.CategoryItemID);
            foreach (var i in SelectCategoryList)


            ViewBag.countriesList = SelectCategoryList;

            var typesList = db.CategoryItems.Where(c => c.category.name.Equals("by Type"));
            SelectList SelectTypeList = new SelectList(typesList, "CategoryItemID", "name",selectedType.CategoryItemID.ToString());

            ViewBag.typesList = SelectTypeList;

            return View(itemHelper);
        }

        //
        // POST: /Items/Edit/5

        [HttpPost]
        public ActionResult Edit(ItemCategoryHelper itemHelper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemHelper.item).State = EntityState.Modified;
                int countryID = int.Parse(itemHelper.SelectedCountry);
                int typeID = int.Parse(itemHelper.SelectedType);
                db.CategoryItems.FirstOrDefault(c => c.CategoryItemID == countryID).items.Add(itemHelper.item);
                db.CategoryItems.FirstOrDefault(c => c.CategoryItemID == typeID).items.Add(itemHelper.item);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = itemHelper.item.ItemID });
            }

            return View(itemHelper.item);
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

        // GET: /Items/_Comments
        public ActionResult Menu()
        {
            return PartialView(db.Categories.Include(x => x.categories));
        }
    }
}