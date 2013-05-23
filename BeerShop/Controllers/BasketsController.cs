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

        public ActionResult AddOrderItem(ViewModelItemIDAmount mvIitemAmount)
        {
            Item item = db.Items.FirstOrDefault(i => i.ItemID == mvIitemAmount.itemID);
            String feedback = "";
            if (item == null )
            {
                return HttpNotFound();             
            }
            else
            {
                Basket basket = null;
                Customer user = (Customer)db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
                if (user != null && user.basket!= null)
                {

                    basket = user.basket;
                    if (basket != null)
                    {
                        try
                        {
                            basket.AddOrderItem(new OrderItem(item, mvIitemAmount.amount));
                            db.SaveChanges();
                            return RedirectToAction("Details");
                        }
                        catch (Exception e)
                        {
                            feedback = e.Message;
                        }
                    }
                    else
                    {
                        feedback = "You have no basket";
                    }
                }
                else
                {
                    feedback = "You are not logged in";
                }
            }
            return RedirectToAction("Details", "Items", new { id = item.ItemID, message = feedback });
              
        }
         // GET: /Baskets/Add?basketId=X&itemId=Y&amount=Z
        public ActionResult Add(int? basketId, int itemId, int amount = 1)
        {
            Basket basket = null;

            if (basketId != null)
                basket = db.Baskets.Find(basketId);
            else
            {
                Customer user = (Customer)db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
                if (user != null)
                {
                    basket = user.basket;
                }
            }
            if (basket == null)
            {
                return HttpNotFound();
            }
            // find item
            Item item = db.Items.Find(itemId);
            if (item == null)
            {
                return HttpNotFound();
            }
            // create orderItem
            OrderItem oItem = new OrderItem(item, amount);

            // add to basket and save
            basket.orderItems.Add(oItem);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = basketId });
            //return View(basket);
        }

        // GET: /Baskets/RemoveItem?basketId=X&ordItemId=Y
        public ActionResult RemoveItem(int basketId, int ordItemId)
        {
            // find the basket
            Basket basket = db.Baskets.Find(basketId);
            if (basket == null)
            {
                return HttpNotFound();
            }
            // find the item in the basket
            OrderItem item = db.OrderItems.Find(ordItemId);
            if (item == null)
            {
                return HttpNotFound();
            }
            if (!basket.orderItems.Contains(item))
            {
                return HttpNotFound();
            }
            basket.orderItems.Remove(item);
            db.OrderItems.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = basketId });
        }

        // GET: /Baskets/IncrementItem?basketId=X&ordItemId=Y
        public ActionResult IncrementItem(int basketId, int ordItemId)
        {
            // find the item
            OrderItem item = GetOrderItem(basketId, ordItemId);
            if (item == null)
            {
                return HttpNotFound();
            }
            if (item.item == null)
            {
                // probably will be never thrown, but it is neccessary to touch item.item
                // as validation would else fails.
                throw new Exception();
            }
            item.amount++;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = basketId });
        }

        // GET: /Baskets/DecrementItem?basketId=X&ordItemId=Y
        public ActionResult DecrementItem(int basketId, int ordItemId)
        {
            // find the item
            OrderItem item = GetOrderItem(basketId, ordItemId);
            if (item == null)
            {
                return HttpNotFound();
            }

            if (item.item == null)
            {
                // probably will be never thrown, but it is neccessary to touch item.item
                // as validation would else fails.
                throw new Exception();
            }
            if (item.amount > 1)
            {
                item.amount--;
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = basketId });
        }

        // find the order item or return null if item does not exists or is not in the basket
        private OrderItem GetOrderItem(int basketId, int ordItemId)
        {
            Basket basket = db.Baskets.Find(basketId);
            if (basket == null)
            {
                return null;
            }
            // find the item in basket
            OrderItem item = db.OrderItems.Find(ordItemId);
            if (item == null)
            {
                return null;
            }
            if (!basket.orderItems.Contains(item))
            {
                return null;
            }
            return item;
        }
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
            Customer user;
            if (User.Identity.Name != null)
                user = (Customer) (from u in db.Users
                        where u.email == User.Identity.Name
                        select u).First();
            else
            {
                return HttpNotFound();
            }
            Basket basket = user.basket;
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        //
        // GET: /Baskets/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //
        // POST: /Baskets/Create

        //[HttpPost]
        //public ActionResult Create(Basket basket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Baskets.Add(basket);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(basket);
        //}

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

        //public ActionResult Delete(int id = 0)
        //{
        //    Basket basket = db.Baskets.Find(id);
        //    if (basket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(basket);
        //}

        //
        // POST: /Baskets/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Basket basket = db.Baskets.Find(id);
        //    db.Baskets.Remove(basket);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        // POST: /Baskets/Create

       // [HttpPost]
        public ActionResult CreateOrder(Basket basket)
        {
            
            return RedirectToAction("Create", "Orders");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult DetailsWidget()
        {
            return PartialView();
        }
    }

}