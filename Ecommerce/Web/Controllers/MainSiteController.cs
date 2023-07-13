using BL.AppServices;
using BL.Hlper;
using BL.ViewModels;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace Web.Controllers
{
    public class MainSiteController : BaseController
    {
       
        Main_CatAppService mainCategoryAppService = new Main_CatAppService();
        ProductAppService ProductAppService = new ProductAppService();
        AccountAppService accountAppService = new AccountAppService();
        SubCategoryAppService SubCategoryAppService = new SubCategoryAppService();
        WishListAppService WishListAppService = new WishListAppService();
        // GET: MainSite
        public ActionResult Index()
        {
            VariationOfProductsViewModel variationOfProductsViewModel = new VariationOfProductsViewModel();
     
         
            List<ProductViewModel> Products = ProductAppService.GetAllBroducts();
            variationOfProductsViewModel.ProductViewModels = Products;
            variationOfProductsViewModel.vendors = accountAppService.GetAllVendors();
          
            
            return View(variationOfProductsViewModel);
        }
        public ActionResult NavBarData()
        {
            if (Session["cart"] != null)
                ViewBag.cartCount = ((List<Item>)Session["cart"]).Count;
            if (User.Identity.IsAuthenticated)
            {
                string id = accountAppService.Find(User.Identity.Name).Id;
                ViewBag.wish = WishListAppService.GetAllWishList().Where(w => w.User_Id == id).Count();
            }
            else
            {
                ViewBag.wish = 0;
            }
            List<Sub_Category> sub_Categories = SubCategoryAppService.GetAllSubCategories().Where(s=>s.Parent_Id==null).ToList();
            return PartialView("_NavBarView", sub_Categories);
        }
       

        public ActionResult ShowProductByCategory(int id)
        {

         List<ProductViewModel> products= ProductAppService.GetAllBroducts().Where(p => p.Sub_Cat_Id == id).ToList();
            return View(products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(SearchViewModel searchViewModel)
        {
            if (searchViewModel.SubCatId!=0)
            {
                List<ProductViewModel> products = ProductAppService.GetAllBroducts().Where(p => p.Sub_Cat_Id == searchViewModel.SubCatId && p.Name==searchViewModel.SearchText).ToList();
                return View("ShowProductByCategory", products);
            }
            else
            {
                List<ProductViewModel> products = ProductAppService.GetAllBroducts().Where(p =>  p.Name == searchViewModel.SearchText.ToLower()).ToList();
                return View("ShowProductByCategory", products);
                
                
             
            }
        }
        public ActionResult GetProductByStore(string vendorID)
        {
          
          
                List<ProductViewModel> products = ProductAppService.GetAllBroducts().Where(p => p.Vendor_User_id == p.Vendor_User_id).ToList();
                return View("ShowProductByCategory", products);
            
        }
        public ActionResult CheckWishList(int product_id)
        {
            string id = accountAppService.Find(User.Identity.Name).Id;
           
             int count= WishListAppService.GetAllWishList().Where(w => w.Product_Id == product_id && w.User_Id == id).Count();
                if (count == 0)
                    return PartialView("_CheckWishListView",false);
                else
                    return PartialView("_CheckWishListView", true);
          
        }
        public ActionResult ProductPatialView(ProductViewModel productView)
        {
            return PartialView("_ProductPartialView", productView);
        }
        public ActionResult ShowWishList()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            string id = accountAppService.Find(User.Identity.Name).Id;
            List<ProductViewModel> products = new List<ProductViewModel>();
         List<WishListViewModel>wishLists=   WishListAppService.GetAllWishList().Where(w => w.User_Id == id).ToList();
            foreach(var w in wishLists)
            {
                products.Add(w.product);
            }
            return View("ShowProductByCategory",products);
        }
        public JsonResult getChildrens(int id)
        {
            helper help = new helper();
            List<SubCategoryViewModelForAjax> child = new List<SubCategoryViewModelForAjax>();
            foreach (var sub in help.showTree(id))
                            {
                child.Add(new SubCategoryViewModelForAjax { Id=sub.Id,Name=sub.Name});
                            }
             return Json(new
            {
                list = child
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getChildrens2(int id)
        {
            helper help = new helper();
            List<Sub_Category> child = new List<Sub_Category>();
            foreach (var sub in help.showTree(id))
            {
                child.Add(sub);
            }
            return PartialView("_HoverDropDownPartialView",child);
        }
        public ActionResult ProductByMainCatIdPartialView(int mainCatId ,string Type)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
           foreach(var sub in SubCategoryAppService.GetSubCatByMainCatId(mainCatId))
            {
              products.AddRange(SubCategoryAppService.GetBroductsBySubCat(sub.Id));
            }
           if(Type=="N")
            return PartialView("_ProductsPartialView", products);
            else
            {
                products.Sort();
                return PartialView("_ProductsPartialView", products);
            }
               
        }
        public ActionResult ChangeLangToArabic()
        {
            Session["SelectedCulture"] = "ar";
            return RedirectToRoute(new { culture = "ar",action="Index",controller="MainSite"});
           //  return RedirectToAction("Index");
        }
        public ActionResult ChangeLangToEnglish()
        {
            Session["SelectedCulture"] = "en";
            return RedirectToRoute(new { culture = "en", action = "Index", controller = "MainSite" });
        }
        public ActionResult GetAllProducts(int pg)
        {
            if (pg < 1)
            {
                pg = 1;
            }
            int count = ProductAppService.GetAllBroductsCount();
            Pager page = new Pager(count, pg, 3);
            int rowskip = (pg - 1) * page.PageSize;
            var data = ProductAppService.GetAllBroductsByParts(rowskip,page.PageSize);
            ViewBag.Pager = page;
            return View(data);
        }
    }
}