using BL.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Model;
using System.Web.Mvc;
using BL.ViewModels;
using System.Reflection;
using System.IO;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        ProductAppService ProductAppService = new ProductAppService();
        BrandAppService BrandAppService = new BrandAppService();
        AccountAppService accountAppService = new AccountAppService();
        SubCategoryAppService subCategoryAppService = new SubCategoryAppService();
        ProductColorAppService productColorAppService = new ProductColorAppService();
        ProductImagesAppService productImagesAppService = new ProductImagesAppService();
        ProductSizeAppService productSizeAppService = new ProductSizeAppService();



        // GET: Product
        public ActionResult Index()
        {
            //var products = ProductAppService.GetAllBroducts();
            /*  foreach (var item in products)
              {
                  item.Brand = BrandAppService.GetBrand(item.Brand_Id);
                  item.Vendor_Name = accountAppService.FindById(item.Vendor_User_id).UserName;
                  item.Sub_Category = subCategoryAppService.GetSubCategory(item.Sub_Cat_Id);
              }*/



            //return View(products);
            return View(new List<ProductViewModel>());
        }
        public ActionResult GetProductDetails(int productId)
        
        
        {
            var product = ProductAppService.GetBroduct(productId);
            ViewData["Brands"] = BrandAppService.GetAllBrand();
           // ViewData["Vendors"] = accountAppService.GetAllVendors();
            ViewData["subCategory"] = subCategoryAppService.GetAllSubCategories().ToList();
            return View(product);
        }
        public ActionResult CreateProduct()
        {
            ViewData["Brands"] = BrandAppService.GetAllBrand();

            ViewData["subCategory"] = subCategoryAppService.GetAllSubCategories().ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(ProductViewModel productViewModel, HttpPostedFileBase imageFile)
        {

            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated && User.IsInRole("Vendor"))
                {
                    productViewModel.Vendor_User_id = accountAppService.GetForEdit(User.Identity.Name).Id;
                }


                ImageUploaderService imageUploaderService = new ImageUploaderService(imageFile, Directories.Products);
                productViewModel.Photo = imageUploaderService.GetImageName();
                imageUploaderService.SaveImage();
                if (productViewModel.Offer_Price > 0)
                {
                    productViewModel.Profit = productViewModel.Price - productViewModel.Offer_Price;
                }
                ProductAppService.SaveNewBroduct(productViewModel);


                TempData["success"] = "Success Add Product";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["failed"] = "failed to Add Product";
                return View(productViewModel);
            }

        }
        public ActionResult UpdateProduct(int id)
        {

            return View(ProductAppService.GetBroduct(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(ProductViewModel productViewModel, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                ImageUploaderService imageUploaderService = new ImageUploaderService(imageFile, Directories.Products);
                productViewModel.Photo = imageUploaderService.GetImageName();
                if (productViewModel.Offer_Price > 0)
                {
                    productViewModel.Profit = productViewModel.Price - productViewModel.Offer_Price;
                }
                var product = ProductAppService.GetBroductModel(productViewModel.Id);
                //var product = Mapper.Map<Product>(productViewModel);
                ProductAppService.UpdateBroduct(product,productViewModel);
                imageUploaderService.SaveImage();
                TempData["success"] = "Success Update Product";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["failed"] = "failed to Update Product";
                ModelState.AddModelError("", "Error in Update");
                return View(productViewModel);
            }

        }

        public ActionResult DeleteProduct(int id)
        {

            ProductAppService.DeleteBroduct(id);
            TempData["successDelete"] = "Success Delete Product";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public JsonResult GetProductSizes(int productId, int? page, int? limit, string sortBy, string direction, decimal price, string size)
        {
            List<ProductSizeDTO> records = new List<ProductSizeDTO>();
            int total = 0;
            if (productId == 0)
            {
                var query = productSizeAppService.GetProductSizeByProductId(productId).Select(p => new ProductSizeDTO
                {
                    Id = p.Id,
                    SizePrice = p.SizePrice,
                    ProductId = p.ProductId,
                    SizeName = p.SizeName,

                });

                if (price != 0)
                {
                    query = query.Where(q => q.SizePrice == price);
                }

                if (!string.IsNullOrWhiteSpace(size))
                {
                    query = query.Where(q => q.SizeName != null && q.SizeName.Contains(size));
                }
                if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
                {
                    if (direction.Trim().ToLower() == "asc")
                    {
                        switch (sortBy.Trim().ToLower())
                        {
                            case "Size":
                                query = query.OrderBy(q => q.SizePrice);
                                break;
                            case "Price":
                                query = query.OrderBy(q => q.SizePrice);
                                break;

                        }
                    }
                    else
                    {
                        switch (sortBy.Trim().ToLower())
                        {

                            case "Size":
                                query = query.OrderByDescending(q => q.SizeName);
                                break;
                            case "Price":
                                query = query.OrderByDescending(q => q.SizePrice);
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
            else
            {
                return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);

            }







        }





        [HttpPost]
        public JsonResult Save(ProductSizeDTO record)
        {
            ProductSizes entity;

            if (record.Id > 0)
            {
                entity = productSizeAppService.GetProductSize(record.Id);
                entity.SizeName = record.SizeName;
                entity.SizePrice = record.SizePrice;
                productSizeAppService.UpdateProductSize(entity);
            }
            else
            {
                productSizeAppService.SaveNewProductSize(new ProductSizeDTO
                {
                    SizePrice = record.SizePrice,
                    SizeName = record.SizeName,
                    ProductId = record.ProductId,

                });
            }

            return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var entity = productSizeAppService.DeleteProductSize(id);
            return Json(new { result = true });
        }




        public JsonResult GetSizes(int? productId, int? page, int? limit)
        {

            List<ProductSizeDTO> records;
            int total;

            var query = productSizeAppService.GetAllProductSize().Where(pt => pt.ProductId == productId).Select(pt => new ProductSizeDTO
            {
                Id = pt.Id,
                ProductId = pt.ProductId,
                SizeName = pt.SizeName,
                SizePrice = pt.SizePrice
            });

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

        public JsonResult GetImages(int? productId, int? page, int? limit)
        {

            List<ProductImagesDTO> records;
            int total;

            var query = productImagesAppService.GetAllProductSize().Where(pt => pt.ProductId == productId).Select(pt => new ProductImagesDTO
            {
                Id = pt.Id,
                ProductId = pt.ProductId,
                ImageURL = $"<img width='300px' height='98px' src='/Content/Imgs/Products/{pt.ImageURL}'/>",
                Name = pt.Name

            });

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
        [HttpPost]
        public JsonResult ImageSave(ProductImagesDTO record, HttpPostedFileBase imageFile)
        {
            ProductImages entity;

            if (Request.Files.Count > 0)
            {

                //  Get all files from Request object  
                HttpFileCollectionBase files = Request.Files;
                string fname = "";
                for (int i = 0; i < files.Count; i++)
                {
                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[i];


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

                    // Get the complete folder path and store the file inside it.  
                    ImageUploaderService imageUploaderService = new ImageUploaderService(file, Directories.Products);
                    record.ImageURL = imageUploaderService.GetImageName();
                    imageUploaderService.SaveImage();
                }


                productImagesAppService.SaveNewProductImages(new ProductImagesDTO
                {
                    Name = fname,
                    ImageURL = record.ImageURL,
                    ProductId = record.ProductId,

                });


                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }
        }

        [HttpPost]
        public JsonResult ImageDelete(int id)
        {
            var entity = productImagesAppService.DeleteProductImages(id);
            return Json(new { result = true });
        }



        [HttpPost]
        public JsonResult SaveColor(ProductColorDTO record)
        {
            ProductColors entity;

            if (record.Id > 0)
            {
                entity = productColorAppService.GetProductColor(record.Id);
                entity.ColorName = record.ColorName;

                productColorAppService.UpdateproductColor(entity);
            }
            else
            {
                productColorAppService.SaveNewProductColor(new ProductColorDTO
                {

                    ColorName = record.ColorName,
                    ProductId = record.ProductId,

                });
            }

            return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult DeleteColor(int id)
        {
            var entity = productColorAppService.DeleteProductColor(id);
            return Json(new { result = true });
        }




        public JsonResult GetColors(int? productId, int? page, int? limit)
        {

            List<ProductColorDTO> records;
            int total;

            var query = productColorAppService.GetAllProductColor().Where(pt => pt.ProductId == productId).Select(pt => new ProductColorDTO
            {
                Id = pt.Id,
                ProductId = pt.ProductId,
                ColorName = pt.ColorName,

            });

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

        public JsonResult GetProducts(int? page, int? limit, string sortBy, string direction,string name, decimal? price)
        {
            List<ProductViewModel> records = new List<ProductViewModel>();
            int total = 0;

            var query = ProductAppService.GetAllBroducts().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Brand_Id = p.Brand_Id,
                Desc = p.Desc,
                Offer_Price = p.Offer_Price,
                Photo = p.Photo,
                Price = p.Price,
                Quantity = p.Quantity,
                Sell_Count = p.Sell_Count,
                Vendor_Name = p.Vendor_Name,
                Vendor_User_id = p.Vendor_User_id,
                Sub_Cat_Id = p.Sub_Cat_Id,
                Profit = p.Profit,
                Active=p.Active

            });

            if (price != null && price !=0 )
            {
                query = query.Where(q => q.Price == price);
            }

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
                        case "Price":
                            query = query.OrderBy(q => q.Price);
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
                        case "Price":
                            query = query.OrderByDescending(q => q.Price);
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
        [HttpPost]
        public JsonResult DeleteProductAjax(int id)
        {
            ProductAppService.DeleteBroduct(id);
            TempData["successDelete"] = "Success Delete Product";
           
            return Json(new { result = true });
        }

    }
}