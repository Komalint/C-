using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVConFramework
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CustomerRoute",
                url: "Customer/{action}/{id}",
                defaults: new { controller = "Customer", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdminFoodRoute",
                url: "AdminFood/{action}/{id}",
                defaults: new { controller = "AdminFood", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "RestaurantRoute",
                url: "Restaurant/{action}/{id}",
                defaults: new { controller = "Restaurant", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeliveryAgentRoute",
                url: "DeliveryAgent/{action}/{id}",
                defaults: new { controller = "DeliveryAgent", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AccountRoute",
                url: "Account/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Customer", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
