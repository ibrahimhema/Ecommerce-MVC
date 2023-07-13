using BL.AppServices;
using BL.ViewModels;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        AccountAppService accountAppService = new AccountAppService();
        ProductAppService productAppService = new ProductAppService();
        CheckOutAppService checkOutAppService = new CheckOutAppService();
        // GET: Profile
        public ActionResult Index()
        {
            var user = accountAppService.FindProfile(User.Identity.Name);
            ViewBag.orders = new List<CheckOutViewModel>();
            if (accountAppService.IsInRole(user.Id, Role_Name.Admin))
            {
                user.Role = Role_Name.Admin.ToString();
               
            }
            else
            {
                ViewBag.orders = checkOutAppService.GetAllCheckOut().Where(o => o.User_Id == user.Id).ToList();
                user.Role = accountAppService.IsInRole(user.Id, Role_Name.Vendor) ? "Vendor" : "User";
            }

            if (user.Role == Role_Name.Vendor.ToString())
            {
                ViewBag.products = productAppService.GetAllBroducts().Where(p => p.Vendor_User_id == user.Id);
            }
            

            return View(user);
        }
    }
}