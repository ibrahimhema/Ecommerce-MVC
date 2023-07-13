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
            checkOutView.Created_at = DateTime.Now;
            checkOutView.Payment_Id = paymentApp.find((Payment_Method)payment).Id;
            checkOutView.Status = Order_Status.Pending;
            
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
    }
}