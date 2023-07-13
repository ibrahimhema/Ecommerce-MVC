using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BaseController : Controller
    {

        protected override void OnActionExecuting(
            ActionExecutingContext filterContext)
        {
            string culture = "";
          
                culture = filterContext.RouteData.Values["culture"]?.ToString()
                             ?? "en";
            
           
           /* if (Session["SelectedCulture"] != null)
            {
                culture = Session["SelectedCulture"].ToString();
            }
            else
            {
                Session["SelectedCulture"] = culture;
            }*/
          /*  string culture = filterContext.RouteData.Values["culture"]?.ToString()
                                ?? "en";*/
            // Set the action parameter just in case we didn't get one
            // from the route.
            filterContext.ActionParameters["culture"] = culture;

            var cultureInfo = CultureInfo.GetCultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            // Because we've overwritten the ActionParameters, we
            // make sure we provide the override to the 
            // base implementation.

            Session["SelectedCulture"] = culture;

            base.OnActionExecuting(filterContext);
        }
        public ActionResult RedirectToLocalized()
        {
            if (Session["SelectedCulture"]!=null)
            {
                return RedirectPermanent("/"+ Session["SelectedCulture"].ToString());
            }
            else
            {
                return RedirectPermanent("/en");
            }
           
        }
    }
}