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

    public class AdminController : Controller
    {
        private BeerShopContext db = new BeerShopContext();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
