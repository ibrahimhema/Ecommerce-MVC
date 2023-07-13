using BL.AppServices;
using BL.ViewModels;
using DAL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AccountController : BaseController
    {

        //Account service 
        readonly AccountAppService accountAppService = new AccountAppService();
         readonly WalletAppService walletAppService = new WalletAppService();
        
        
        public ActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel newUser, HttpPostedFileBase imageFile)
        {
            if (!ModelState.IsValid)
                return View(newUser);
            if (accountAppService.HasEmail(newUser.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(newUser);
            }

            ImageUploaderService imageUploader = new ImageUploaderService(imageFile, Directories.Users);

            newUser.Photo = imageUploader.GetImageName();

            IdentityResult result = accountAppService.Register(newUser);
            if (result.Succeeded)
            {
                imageUploader.SaveImage();

                var user = accountAppService.FindByEmailAndPassword(newUser.Email, newUser.PasswordHash);
                walletAppService.SaveNewWallet(new Wallet { User_Id=user.Id});
                IAuthenticationManager owinManager = HttpContext.GetOwinContext().Authentication;
                //SignIn

                SignInManager<ApplicationUser, string> signInManager = new SignInManager<ApplicationUser, string>(
                    new ApplicationUserManager(),owinManager
                    );

                //assign role to user
                accountAppService.AssignToRole(user.Id, newUser.Role);

                //checkbox for rememberMe
              
                signInManager.SignIn(user, newUser.RememberMe,newUser.RememberMe);
            

                return RedirectToAction("Index","MainSite");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(newUser);
            }
                
        }


        public ActionResult Login(string ReturnUrl) 
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel user, string ReturnUrl)
        {
            if (ModelState.IsValid == false)
                return View(user);


            ApplicationUser identityUser = accountAppService.FindByEmailAndPassword(user.Email, user.PasswordHash);

            if (identityUser != null)
            {
                IAuthenticationManager owinMAnager = HttpContext.GetOwinContext().Authentication;
                //SignIn
                SignInManager<ApplicationUser, string> signinmanager =
                    new SignInManager<ApplicationUser, string>(
                        new ApplicationUserManager(), owinMAnager
                        );
                signinmanager.SignIn(identityUser, user.RememberMe, user.RememberMe);

                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Mainsite");
            }
            else
            {
                ModelState.AddModelError("", "Not Valid Username & Password");
                return View(user);
            }

        }
        [Authorize]
        public ActionResult Logout()
        {
            IAuthenticationManager owinMAnager = HttpContext.GetOwinContext().Authentication;
            owinMAnager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session["cart"] = null;
            return RedirectToAction("Index", "Mainsite");
        }

        [Authorize]
        public ActionResult Edit()
        {
            ProfileEditViewModel user = accountAppService.GetForEdit(User.Identity.Name);
            return View(user);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Edit(ProfileEditViewModel userInfo, HttpPostedFileBase imageFile)
        {
            ImageUploaderService imageUploaderService;
            var user = accountAppService.Find(User.Identity.Name);

            if (imageFile != null)
            {
                imageUploaderService = new ImageUploaderService(imageFile, Directories.Users);
                //delete old image if its not the default
                if (user.Photo.Contains("default"))
                {
                    ImageUploaderService.DeleteImage(user.Photo, Directories.Users);
                }
                //save new image name to user
                user.Photo = imageUploaderService.GetImageName();
                //save new image to serve
                imageUploaderService.SaveImage();
            }

            user.Address = userInfo.Address;
            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;

            var result = accountAppService.Edit(user);

            if (result.Succeeded)
            {
                ImageUploaderService.RecreateFolder(Directories.Temp);
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                return View(userInfo);
            }
        }

    }
}