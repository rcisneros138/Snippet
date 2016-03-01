using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Snippets
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "chromeUrlPass",
                url: "{controller}/{action}/{URL}",
                defaults: new { controller = "Snippets", action = "ChromeCreate", URL = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
                );
            routes.MapRoute(
                name: "extensionview",
                url: "{controller}/{action}/{Id}",
                defaults: new { Controller = "Snippets", Action = "extensionView", Id = UrlParameter.Optional }
                );
        }
    }
}
