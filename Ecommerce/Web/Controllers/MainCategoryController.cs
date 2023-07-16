using BL.AppServices;
using DAL.Model;
using BL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class MainCategoryController : BaseController
    {
        readonly Main_CatAppService main_CatApp = new Main_CatAppService();
        // GET: MainCategory
        public ActionResult Index()
        {
            return View(main_CatApp.GetAllMain_Category());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public JsonResult Create(MainCategoryViewModelForAjax main_Cat, HttpPostedFileBase imageFile)
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

                ImageUploaderService imageUploader = new ImageUploaderService(file, Directories.Main_Categories);
                main_Cat.Photo = imageUploader.GetImageName();
                imageUploader.SaveImage();
            }
            if(main_Cat.Id==null ||  main_Cat.Id == 0)
            {
                main_CatApp.SaveNewMain_Category(main_Cat);
            }
            else
            {
                var mainCat = main_CatApp.GetMain_Category(main_Cat.Id.Value);
                main_CatApp.UpdateMain_Category(mainCat, main_Cat);
              
            }

          
            
    

            return Json(new { result = true });
        }

        public ActionResult Edit(int id)
        {
            return View(main_CatApp.GetMain_Category(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MainCategoryViewModelForAjax main_Cat, HttpPostedFileBase imageFile)
        {
            ImageUploaderService imageUploader = new ImageUploaderService(imageFile, Directories.Main_Categories);
            main_Cat.Photo = imageUploader.GetImageName();
            if (!ModelState.IsValid)
            {
                return View(main_Cat);
            }
            var mainCat = main_CatApp.GetMain_Category(main_Cat.Id.Value);
            main_CatApp.UpdateMain_Category(mainCat,main_Cat);
            imageUploader.SaveImage();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(main_CatApp.GetMain_Category(id));
        }

        public ActionResult Delete(int id)
        {
            main_CatApp.DeleteMain_Category(id);
            return RedirectToAction("Index");
        }







        [HttpGet]

        public JsonResult GetMainCategory(int? page, int? limit, string sortBy, string direction, string name)
        {
            List<MainCategoryViewModelForAjax> records = new List<MainCategoryViewModelForAjax>();
            int total = 0;

            var query = main_CatApp.GetAllMain_Category().Select(p => new MainCategoryViewModelForAjax
            {
                Id = p.Id,
                Name = p.Name,
             
               
                Photo = $"<img width='100px' height='50px' src='/Content/Imgs/Main_Categories/{p.Photo}'/>",

                Active = p.Avtive

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

                        case "Size":
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