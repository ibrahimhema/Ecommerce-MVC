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
    public class CheckOutController : BaseController
    {
        CheckOutAppService checkOutApp = new CheckOutAppService();
        AccountAppService accountAppService = new AccountAppService();
        PaymentAppService paymentApp = new PaymentAppService();
        ProductAppService productAppService = new ProductAppService();
        OrderDetailsAppService orderDetailsApp = new OrderDetailsAppService();

        [Authorize(Roles ="Admin")]
        // GET: CheckOut
        public ActionResult Index(int? RowId)
        {
            if(RowId != null)
            {
             
                 
                
                return new RedirectResult(Url.Action("Index") + "#"+RowId);
            }
         
            
                return View(checkOutApp.GetAllCheckOut());
            
           
        }
        public ActionResult GetOrdersByStutesPartialView(int Stutes)
        {

            return PartialView("_TableOrdersPartialView",checkOutApp.GetCheckOutByStutes((Order_Status)Stutes));
        }
        public ActionResult GetOrdersByDatePartialView(DateTime from,DateTime to)
        {

            return PartialView("_TableOrdersPartialView", checkOutApp.GetCheckOutByDate(from,to));
        }
        [HttpPost]
        public ActionResult AddCheckOut(CheckOutViewModel checkOutView,OrderDetailsViewModel orderDetailsView, int payment)
        {

           
            checkOutView.User_Id = accountAppService.Find(User.Identity.Name).Id;
           // checkOutView.Created_at = DateTime.Now;
           // checkOutView.Payment_Id = paymentApp.find((Payment_Method)payment).Id;
           // checkOutView.Status = Order_Status.Pending;
            
            List<Item> cart = (List<Item>)Session["cart"];
           int CheckId=checkOutApp.SaveNewCheckOut(checkOutView);
            if (CheckId > 0)
            {
                foreach (var product in cart)
                {
                    orderDetailsView.Product_Id = product.Product.Id;
                   
                    orderDetailsView.Quantity = product.Quantity;
                    orderDetailsView.Order_Id = CheckId;
                    orderDetailsApp.SaveNewOrderDetail(orderDetailsView);
                }
            }
       
            Session.Remove("cart");
            return RedirectToAction("Index","MainSite");

        }


        public ActionResult Edit(int id,int? rowId)
        {
            if (rowId != null)
            {
                ViewBag.RowId = rowId;
            }
            else
            {
                ViewBag.RowId = 0;
            }
            var order = checkOutApp.GetCheckOut(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(CheckOutViewModel order)
        {
            if (!ModelState.IsValid)
                return View(order);
            checkOutApp.UpdateCheckOut(order);
            //return RedirectToAction("Index");
            return new RedirectResult(Url.Action("Index") + "#" + order.RowId);
        }
        public ActionResult GetOrdersDetailsByOrderId(int OrderId)
        {
            List<Order_Product> lst = orderDetailsApp.GetOrderDetailByOrderId(OrderId);
            return PartialView("ModelBodyForOrderDetailsPartialView", lst);
        }



   
        // GET: CheckOut
       
        [Authorize(Roles = "Admin")]
        public JsonResult GetCheckout(int? page, int? limit, string sortBy, string direction, DateTime? Created_at, int? status, DateTime? from, DateTime? to)
        {
            List<CheckOutViewModel> records = new List<CheckOutViewModel>();
            int total = 0;

            var query = checkOutApp.GetAllCheckOut().Select(x => new CheckOutViewModel
            {

                Id = x.Id,
               Address=x.Address,
               Created_at=x.Created_at,
               Payment_Id =( (Payment_Method)x.Payment.Payment_Method).ToString(),
               Status = x.Status.ToString(),
               Total_Price=x.Total_Price,
               UserName=x.User.FirstName
                //Photo = $"<img width='100px' height='50px' src='/Content/Imgs/Sub_Categories/{x.Photo}'/>"

            });



            if (status!=null)
            {
                query = query.Where(q =>  q.Status==((Order_Status)status).ToString());
            }
            if(from!=null && to == null)
            {
                query = query.Where(q => q.Created_at >= from );
            }
            if (to != null && from == null)
            {
                query = query.Where(q => q.Created_at <= to);
            }
            if (from != null && to !=null)
            {
                query = query.Where(q=>q.Created_at >=from && q.Created_at<=to);
            }
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "status":
                            query = query.OrderBy(q => q.Status);
                            break;
                        case "Created_at":
                            query = query.OrderBy(q => q.Created_at);
                            break;

                    }
                }
                else
                {
                    switch (sortBy.Trim().ToLower())
                    {

                        case "status":
                            query = query.OrderByDescending(q => q.Status);
                            break;
                        case "Created_at":
                            query = query.OrderByDescending(q => q.Created_at);
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