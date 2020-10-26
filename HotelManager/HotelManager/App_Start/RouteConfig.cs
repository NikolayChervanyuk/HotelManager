using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotelManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "HomeView",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "UserRole", id = UrlParameter.Optional }
            );
            routes.MapMvcAttributeRoutes();
            /*routes.MapRoute(
                name: "ManagerNewReservationRoute",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Manager", action = "ManagerNewReservationSave", }
                )*/
            /*routes.MapRoute(
                name: "AdminLogin",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "AdminLogin", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "AdminSearch",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "AdminSearch", id = UrlParameter.Optional }
                );*/
        }
    }
}
