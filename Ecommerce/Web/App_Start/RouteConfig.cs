﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Mainsite", action = "Index", id = UrlParameter.Optional }
             );

            routes.MapRoute(
               name: "Root",
               url: "",
               defaults: new
               {
                   controller = "Base",
                   action = "RedirectToLocalized"
               }
           );
            routes.MapRoute(
                name: "Default2",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new
                {
                    culture = "en",
                    controller = "Mainsite",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                constraints: new { culture = "en|ar" }
            );
        }
    }
}
