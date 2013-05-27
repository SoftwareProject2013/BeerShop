using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerShop.Models;
using PagedList;
using System.Net;
using System.IO;

namespace BeerShop.Controllers
{
    public class ItemsController : Controller
    {
        private BeerShopContext db = new BeerShopContext();
        

        public ActionResult BootstrapIndex()
        {
            return RedirectToAction("Index", "Items");
        }
        //
        // GET: /Items/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string categoryType, string category, bool? clearDictionary, string message="")
        {

            
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.PriceSort = sortOrder == "Price" ? "Price desc" : "Price";

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
            if (categoryDictionary == null || (clearDictionary ?? false) == true)
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
                SQLQuerry = SQLQuerry.Substring(0, SQLQuerry.Length - 10) + ";";
                var query = db.Items.SqlQuery(SQLQuerry).ToList();
                items = query.AsQueryable();
            }



            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Name desc":
                    items = items.OrderByDescending(i => i.name);
                    break;
     
                case "Price":
                    items = items.OrderByDescending(i => i.Price);
                    break;
                case "Price desc":
                    items = items.OrderBy(i => i.Price);
                    break;
                default:
                    items = items.OrderBy(i => i.name);
                    break;
            }

            ViewBag.message = message;
            int PageSize = 7;
            int pagenumber = (page ?? 1);           
            return View(items.ToPagedList(pagenumber, PageSize));

        }

        //
        // GET: /Items/Details/5
        public ActionResult Details(int id = 0, string message ="", int editableCommentID=0)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Admin"))
            {
                ViewBag.PermissionLevel = Worker.masterPermission;
            }
            ItemCategoryHelper itemHelper = new ItemCategoryHelper();
            itemHelper.item = item;
            foreach (var categoryType in db.Categories)
            {
                string itemCategory = "";
                try
                {
                    itemCategory = item.categories.FirstOrDefault(c => c.category.name.Equals(categoryType.name)).name.ToString();
                }
                catch (Exception ex)
                {
                    itemCategory = "no selected";
                }
                itemHelper.categoryTypeCategoryDictionary.Add(categoryType.name, itemCategory);
            }

                itemHelper.selectedCommentID = editableCommentID;
                
                ViewBag.message = message;


            return View(itemHelper);
        }

        //
        // GET: /Items/Create
        [Authorize(Roles="Admin")]
        public ActionResult Create()
        {
 
                Dictionary<string, SelectList> categoriesDictionary = new Dictionary<string, SelectList>();
                foreach (var categoryType in db.Categories)
                {
                    SelectList SelectCategoryList = new SelectList(categoryType.categories, "CategoryItemID", "name");
                    var catItemNoSelected = new CategoryItem();
                    catItemNoSelected.CategoryItemID = -1;
                    catItemNoSelected.name = "no selected";
                    categoryType.categories.Add(catItemNoSelected);
                
                    categoriesDictionary.Add(categoryType.name, SelectCategoryList);
                }

                ViewBag.typesList = categoriesDictionary;
                return View();
        }

        //
        // POST: /Items/Create
        
        [HttpPost]
        [Authorize(Roles="Admin")]
        public ActionResult Create(ItemCategoryHelper itemHelper)
        {
                if (ModelState.IsValid)
                {
                    itemHelper.item.imageURL = GetItemPicture(itemHelper.item.name);
                    itemHelper.item.isStillOnSale = true;
                    db.Items.Add(itemHelper.item);
                    foreach (var selectedCategory in itemHelper.categoryTypeCategoryDictionary)
                    {

                        if (selectedCategory.Value.Equals("-1"))
                            break;
                        int selectedCategoryID = int.Parse(selectedCategory.Value);
                        db.CategoryItems.FirstOrDefault(c => c.CategoryItemID == selectedCategoryID).items.Add(itemHelper.item);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(itemHelper.item);
 
        }

        private static string GetItemPicture(String name)
        {
            string url = "https://www.google.com/search?hl=pl&site=imghp&tbm=isch&source=hp&biw=1366&bih=641&q=" + "beer+" + name +"&og=beer+"+ name ;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string imgUrl = "http://upload.wikimedia.org/wikipedia/commons/e/e3/NCI_Visuals_Food_Beer.jpg";
            using (HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = httpWebResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string html = reader.ReadToEnd(); // here we go, we sent a request  using the steps above and saved the response in our StreamReader
                        String[] s = html.Split(new String[] { "&amp" }, StringSplitOptions.RemoveEmptyEntries)
                                                                             .Where(data => data.Contains("imgres?imgurl="))
                                                                             .Select(data => data.Substring(3)).ToArray();
                        for (int i = 0; i < s.Count(); i++) // loop the string array 
                        {
                            imgUrl = s[i].Remove(0, s[i].IndexOf("imgurl=") + 7);
                        }
                    }
                }
            }
            return imgUrl;
        }

        //
        // GET: /Items/Edit/5
        [Authorize(Roles="Admin")]
        public ActionResult Edit(int id = 0)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ItemCategoryHelper itemHelper = new ItemCategoryHelper();
            itemHelper.item = item;
            Dictionary<string, SelectList> categoriesDictionary = new Dictionary<string, SelectList>();
            foreach (var categoryType in db.Categories)
            {
                string selectedValue = "";
                var catItemNoSelected = new CategoryItem();
                catItemNoSelected.CategoryItemID = -1;
                catItemNoSelected.name = "no selected";
                categoryType.categories.Add(catItemNoSelected);
                try
                {
                    selectedValue = item.categories.FirstOrDefault(c => c.category.name.Equals(categoryType.name)).name;
                }
                catch (Exception e)
                {
                    selectedValue = catItemNoSelected.name;
                }
                SelectList SelectCategoryList = new SelectList(categoryType.categories, "CategoryItemID", "name", selectedValue);
                categoriesDictionary.Add(categoryType.name, SelectCategoryList);
            }

            ViewBag.categoriesDictionary = categoriesDictionary;

            return View(itemHelper);
    
        }



        //
        // POST: /Items/Edit/5

        [HttpPost]
        [Authorize(Roles="Admin")]
        public ActionResult Edit(ItemCategoryHelper itemHelper)
        {
                if (ModelState.IsValid)
                {
                    db.Entry(itemHelper.item).State = EntityState.Modified;

                    var itemTMP = db.Items.Find(itemHelper.item.ItemID);
                    db.Entry(itemTMP).Collection(i => i.categories).Load();

                    itemTMP.categories.ToList().ForEach(cat => itemTMP.categories.Remove(cat));

                    foreach (var selectedCategory in itemHelper.categoryTypeCategoryDictionary)
                    {
                        if (selectedCategory.Value.Equals("-1"))
                            continue;
                        int selectedCategoryID = int.Parse(selectedCategory.Value);
                        db.CategoryItems.FirstOrDefault(c => c.CategoryItemID == selectedCategoryID).items.Add(itemHelper.item);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = itemHelper.item.ItemID });
                }

                return View(itemHelper.item);
            
        }

        //
        // GET: /Items/Delete/5
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
                Item item = db.Items.Find(id);
                db.Entry(item).Collection(i => i.categories).Load();

                item.categories.ToList().ForEach(cat => item.categories.Remove(cat));
                var comments = db.Comments;
                foreach (var c in item.comments)
                {
                    comments.Remove(c);
                }
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