using BL.AppServices;
using BL.ViewModels;
using DAL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //Account service 
        readonly AccountAppService accountAppService = new AccountAppService();

        ProductAppService ProductAppService = new ProductAppService();
       
        SubCategoryAppService subCategoryAppService = new SubCategoryAppService();

        public ActionResult Index()
        {
           ViewBag.products= ProductAppService.GetAllBroducts().Count();
            ViewBag.users = accountAppService.GetAllUsers().Count();
            ViewBag.vendors = accountAppService.GetAllVendors().Count();
            return View();
        }

        public ActionResult RegisterAdmin() => View();

        [HttpPost]
        public ActionResult RegisterAdmin(RegisterViewModel newUser, HttpPostedFileBase imageFile)
        {
            if (!ModelState.IsValid)
                return View(newUser);

            if (accountAppService.HasEmail(newUser.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(newUser);
            }

            ImageUploaderService imageUploaderService = new ImageUploaderService(imageFile, Directories.Users);
            newUser.Photo = imageUploaderService.GetImageName();

            IdentityResult result = accountAppService.Register(newUser);
            if (result.Succeeded)
            {
                if (imageFile != null)
                    imageUploaderService.SaveImage();

                var user = accountAppService.Find(newUser.Username, newUser.PasswordHash);

                //assign admin role to user
                IdentityResult res = accountAppService.AssignToRole(user.Id, Role_Name.Admin);
                if (res.Succeeded) {


                    /////clear temp image in temp directory
                    ImageUploaderService.RecreateFolder(Directories.Temp);

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Can't assign role Admin to user");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }


            return View(newUser);
        }


        public ActionResult RegisterAdminById(string id)
        {
            
            var user = accountAppService.FindById(id);

            if (user != null)
            {
             
                

                if (accountAppService.IsInRole(user.Id, Role_Name.Admin))
                {
                    //alert user is already an admin
                } 
                else
                {
                    IdentityResult res = accountAppService.AssignToRole(user.Id, Role_Name.Admin);
                    if (res.Succeeded)
                        return RedirectToAction("Index");
                }
            }
            else
            {
                //no user for the id Alert
            }

            return RedirectToAction("Admins");
        }

        [AllowAnonymous]
        public ActionResult Login() => RedirectToAction("Login","Account");


        public ActionResult Logout() => RedirectToAction("Logout", "Account");


        public ActionResult DeleteAdmin(string id)
        {
            if (accountAppService.IsInRole(id, Role_Name.Admin))
                accountAppService.RemoveFromRole(id, Role_Name.Admin.ToString());

            return RedirectToAction("Admins");
        }

        public ActionResult Admins()
        {
            ViewBag.title = "Users";
            return View();
        }

        
        public ActionResult Users()
        {
            ViewBag.title = "Users";
            return View();
        }

        public ActionResult Vendors()
        {
            ViewBag.title = "Vendors";
            return View();
        }



        [HttpGet]

        public JsonResult GetVendors(int? page, int? limit, string sortBy, string direction, string name, string email)
        {
            List<AdminDisplayUserViewModel> records = new List<AdminDisplayUserViewModel>();
            int total = 0;

            var query = accountAppService.GetAllVendorsData().Select(p => new AdminDisplayUserViewModel
            {
            
                Active = p.Active,
                Created_at=p.Created_at,
                Email=p.Email,
                FirstName=p.FirstName,
                LastName=p.LastName,
                ID=p.Id,
                Photo=p.Photo,
                Products=p.Products.Count

            });

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(q => q.FirstName != null && q.FirstName.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(q => q.Email != null && q.Email.Contains(email));
            }
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "name":
                            query = query.OrderBy(q => q.FirstName);
                            break;
                        case "email":
                            query = query.OrderBy(q => q.Email);
                            break;

                    }
                }
                else
                {
                    switch (sortBy.Trim().ToLower())
                    {

                        case "name":
                            query = query.OrderByDescending(q => q.FirstName);
                            break;
                        case "email":
                            query = query.OrderByDescending(q => q.Email);
                            break;
                    }
                }
            }
            else
            {
                query = query.OrderBy(q => q.ID);
            }

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }
            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]

        public JsonResult GetUsers(int? page, int? limit, string sortBy, string direction, string name, string email)
        {
            List<AdminDisplayUserViewModel> records = new List<AdminDisplayUserViewModel>();
            int total = 0;

            var query = accountAppService.GetAllUsersData().Select(p => new AdminDisplayUserViewModel
            {

                Active = p.Active,
                Created_at = p.Created_at,
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                ID = p.Id,
                Photo = p.Photo,
                Products = p.Products.Count,
                Orders=p.Orders.Count

            });

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(q => q.FirstName != null && q.FirstName.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(q => q.Email != null && q.Email.Contains(email));
            }
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "name":
                            query = query.OrderBy(q => q.FirstName);
                            break;
                        case "email":
                            query = query.OrderBy(q => q.Email);
                            break;

                    }
                }
                else
                {
                    switch (sortBy.Trim().ToLower())
                    {

                        case "name":
                            query = query.OrderByDescending(q => q.FirstName);
                            break;
                        case "email":
                            query = query.OrderByDescending(q => q.Email);
                            break;
                    }
                }
            }
            else
            {
                query = query.OrderBy(q => q.ID);
            }

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }
            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAdmins(int? page, int? limit, string sortBy, string direction, string name, string email)
        {
            List<AdminDisplayUserViewModel> records = new List<AdminDisplayUserViewModel>();
            int total = 0;

            var query = accountAppService.GetAllAdmins().Select(p => new AdminDisplayUserViewModel
            {

                Active = p.Active,
                Created_at = p.Created_at,
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                ID = p.Id,
                Photo = p.Photo,
              

            });

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(q => q.FirstName != null && q.FirstName.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(q => q.Email != null && q.Email.Contains(email));
            }
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "name":
                            query = query.OrderBy(q => q.FirstName);
                            break;
                        case "email":
                            query = query.OrderBy(q => q.Email);
                            break;

                    }
                }
                else
                {
                    switch (sortBy.Trim().ToLower())
                    {

                        case "name":
                            query = query.OrderByDescending(q => q.FirstName);
                            break;
                        case "email":
                            query = query.OrderByDescending(q => q.Email);
                            break;
                    }
                }
            }
            else
            {
                query = query.OrderBy(q => q.ID);
            }

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }
            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);

        }
    }
}