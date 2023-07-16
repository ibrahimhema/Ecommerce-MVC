using BL.AppServices;
using BL.ViewModels;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{

    public class SubCategoryController : BaseController
    {
        // GET: SubCategory
        //Admin
         SubCategoryAppService subCategory = new SubCategoryAppService();
         Main_CatAppService main_CatApp = new Main_CatAppService();
        public ActionResult Index()
        {
            //ViewBag.Main = main_CatApp.GetAllMain_Category();
            List<Sub_Category> sub_Categories = subCategory.GetAllSubCategories().ToList();
            foreach( var item in sub_Categories) 
            {
                item.Main_Category = main_CatApp.GetMain_Category(item.Cat_Id);
            }
            return View(sub_Categories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Parent = subCategory.GetAllSubCategories().ToList();
            ViewBag.Main = main_CatApp.GetAllMain_Category();
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create( SubCategoryViewModel sub_Category)
        {

            if (!ModelState.IsValid)
            {

                return Json(new { result = false });
            }

            if (Request.Files.Count > 0)
            {

                //  Get all files from Request object  
                HttpFileCollectionBase files = Request.Files;
                string fname = "";
                HttpPostedFileBase file = files[0];


                // Checking for Internet Explorer  
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }

                ImageUploaderService imageUploader = new ImageUploaderService(file, Directories.Sub_Categories);
                sub_Category.Photo = imageUploader.GetImageName();
                imageUploader.SaveImage();
            }

            if (sub_Category.Parent_Id == 0)
            {
                sub_Category.Parent_Id = null;
            }

            if (sub_Category.Id == null || sub_Category.Id == 0)
            {
                subCategory.SaveNewSubCategory(sub_Category);
            }
            else
            {
                var mainCat = subCategory.GetSubCategory(sub_Category.Id.Value);
                subCategory.UpdateSubCategory(mainCat, sub_Category);

            }


            return Json(new { result = true });
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Parent = subCategory.GetAllSubCategories().Where(s => s.Id != id).ToList();
            ViewBag.Main = main_CatApp.GetAllMain_Category();
            return View(subCategory.GetSubCategory(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, Sub_Category sub_Category)
        {
            ViewBag.Parent = subCategory.GetAllSubCategories().Where(s => s.Id != sub_Category.Id).ToList();
            ViewBag.Main = main_CatApp.GetAllMain_Category();
            if (!ModelState.IsValid)
            {
                return View(sub_Category);
            }
            if (file != null)
            {
                ImageUploaderService imageupload = new ImageUploaderService(file, Directories.Sub_Categories);
                ImageUploaderService.DeleteImage(sub_Category.Photo, Directories.Sub_Categories);
                imageupload.SaveImage();
                sub_Category.Photo = imageupload.GetImageName();
               
            }
            if (sub_Category.Parent_Id == 0)
            {
                sub_Category.Parent_Id = null;
            }

            try
            {
                subCategory.UpdateNormal(sub_Category);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(sub_Category);
            }
            return RedirectToAction("Index", "SubCategory");
        }
        public ActionResult Delete(int id)
        {
            var sup = subCategory.GetSubCategory(id);
            if (sup != null)
            {
                var parent = subCategory.GetAllSubCategories().Where(x=>x.Parent_Id== sup.Id).ToList();
                if (parent.Count> 0 )
                {
                    return Json(new { result = false,message=$"Cant Delete this category you must delete {parent.FirstOrDefault()?.Name} first" });
                }
            }
            subCategory.DeleteSubCategory(id);
            return Json(new { result = true });
        }

        public JsonResult GetSubCategory()
        {
            //ViewBag.Main = main_CatApp.GetAllMain_Category();
            var sub_Categories = subCategory.GetAllSubCategories().Select(x => new {

                Id = x.Id,
                Name = x.Name,
                MainCatName=x.Main_Category.Name,
                ParentCatName=x.Parent.Name,
                Parent_Id=x.Parent_Id,
                Cat_Id=x.Cat_Id,
                Photo=x.Photo

            }).ToList();


            return Json(new {records= sub_Categories }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubCategoryAjax()
        {
            //ViewBag.Main = main_CatApp.GetAllMain_Category();
            var sub_Categories = subCategory.GetAllSubCategories().Select(x=>new {

                value = x.Id,
                text = x.Name

            }).ToList();
        

            return Json(sub_Categories , JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMainCategoryAjax()
        {
            //ViewBag.Main = main_CatApp.GetAllMain_Category();
            var main_Categories = main_CatApp.GetAllMain_Category().Select(x => new {

                value = x.Id,
                text = x.Name

            }).ToList();

            return Json( main_Categories , JsonRequestBehavior.AllowGet);
        }
        [HttpGet]

        public JsonResult GetSubCategory(int? page, int? limit, string sortBy, string direction, string name)
        {
            List<SubCategoryViewModel> records = new List<SubCategoryViewModel>();
            int total = 0;

            var query = subCategory.GetAllSubCategories().Select(x => new SubCategoryViewModel
            {

                Id = x.Id,
                Name = x.Name,
                MainCatName = x.Main_Category.Name,
                ParentCatName = x.Parent.Name,
                Parent_Id = x.Parent_Id,
                Cat_Id = x.Cat_Id,
                Photo=x.Photo,
                Active=x.Active
                //Photo = $"<img width='100px' height='50px' src='/Content/Imgs/Sub_Categories/{x.Photo}'/>"

            });



            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(q => q.Name != null && q.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "name":
                            query = query.OrderBy(q => q.Name);
                            break;


                    }
                }
                else
                {
                    switch (sortBy.Trim().ToLower())
                    {

                        case "name":
                            query = query.OrderByDescending(q => q.Name);
                            break;

                    }
                }
            }
            else
            {
                query = query.OrderBy(q => q.Id);
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