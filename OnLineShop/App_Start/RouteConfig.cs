using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnLineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{id}",
                defaults: new { controller = "ProductCategory", action = "GetCategoryById", id = UrlParameter.Optional },
                namespaces: new[] { "OnLineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new[] { "OnLineShop.Controllers" }
            );
        }
    }
}
