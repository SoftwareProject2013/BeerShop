using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BeerShop.Controllers;
using NavigationRoutes;

namespace BootstrapMvcSample
{
    public class ExampleLayoutsRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapNavigationRoute<HomeController>("Show Categories", c=> c.About()).
                AddChildRoute<CategoriesController>("Category Index", c=>c.Index()).
                AddChildRoute<CategoriesController>("Create Category", c=>c.Create()).
                AddChildRoute<CategoryItemsController>("Show subcategories" , c=> c.Index()).
                AddChildRoute<CategoryItemsController>("Create subcategory", c=>c.Create());
            routes.MapNavigationRoute<HomeController>("Items", c => c.Contact()).
                AddChildRoute<ItemsController>("Show items list", c => c.BootstrapIndex()).
                AddChildRoute<ItemsController>("Add new item", c => c.Create());


        }
    }
}
