using BL.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Model;
using System.Web.Mvc;
using BL.ViewModels;
using System.Reflection;

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
            var products = ProductAppService.GetAllBroducts();
          /*  foreach (var item in products)
            {
                item.Brand = BrandAppService.GetBrand(item.Brand_Id);
                item.Vendor_Name = accountAppService.FindById(item.Vendor_User_id).UserName;
                item.Sub_Category = subCategoryAppService.GetSubCategory(item.Sub_Cat_Id);
            }*/



            return View(products);
        }
        public ActionResult CreateProduct()
        {
            ViewData["Brands"] = BrandAppService.GetAllBrand();
         ViewData["Vendors"] = accountAppService.GetAllVendors();
            ViewData["subCategory"] = subCategoryAppService.GetAllSubCategories();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(ProductViewModel productViewModel, HttpPostedFileBase imageFile)
        {
            ViewData["Brands"] = BrandAppService.GetAllBrand();
            if (ModelState.IsValid)
            {
                ImageUploaderService imageUploaderService = new ImageUploaderService(imageFile, Directories.Products);
                productViewModel.Photo = imageUploaderService.GetImageName();
                imageUploaderService.SaveImage();
                if (productViewModel.Offer_Price>0) {
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
                ProductAppService.UpdateBroduct(productViewModel);
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
        public JsonResult GetProductSizes(int? page, int? limit, string sortBy, string direction, decimal price, string size)
        {
            List<ProductSizeDTO> records;
            int total;

            var query = productSizeAppService.GetAllProductSize().Select(p => new ProductSizeDTO
            {
                Id = p.Id,
                Price = p.Price,
                ProductId = p.ProductId,
                Size = p.Size,

            });

            if (price != 0)
            {
                query = query.Where(q => q.Price == price);
            }

            if (!string.IsNullOrWhiteSpace(size))
            {
                query = query.Where(q => q.Size != null && q.Size.Contains(size));
            }
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "Size":
                            query = query.OrderBy(q => q.Size);
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
                            query = query.OrderByDescending(q => q.Size);
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
        

            return this.Json(new { records, total}, JsonRequestBehavior.AllowGet);




        }





        [HttpPost]
        public JsonResult Save(ProductSizeDTO record)
        {
            ProductSizeDTO entity;
          
                if (record.Id > 0)
                {
                    entity = productSizeAppService.GetProductSize(record.Id);
                    entity.Size = record.Size;
                    entity.Price = record.Price;
                    
                }
                else
                {
                productSizeAppService.SaveNewProductSize(new ProductSizeDTO
                    {
                        Price = record.Price,
                        Size = record.Size,
                        ProductId = record.ProductId,
                        
                    });
                } 
            
            return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
                var entity=productSizeAppService.DeleteProductSize(id);
            return Json(new { result = true });
        }




        public JsonResult GetSizes(int productId, int? page, int? limit)
        {
            List<ProductSizeDTO> records;
            int total;
           
                var query =productSizeAppService.GetAllProductSize().Where(pt => pt.ProductId == productId).Select(pt => new ProductSizeDTO
                {
                    Id = pt.Id,
                  ProductId=pt.ProductId,
                  Size = pt.Size,
                  Price = pt.Price
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

    }
}