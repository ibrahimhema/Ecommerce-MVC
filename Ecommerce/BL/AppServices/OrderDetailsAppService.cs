using BL.Bases;
using BL.ViewModels;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
   public class OrderDetailsAppService: BaseAppService
    {
        #region CURD

        public List<OrderDetailsViewModel> GetAllOrderDetail()
        {
            return Mapper.Map<List<OrderDetailsViewModel>>(TheUnitOfWork.OrderDetails.GetAllOrderDetails());
        }
        public OrderDetailsViewModel GetOrderDetail(int id)
        {
            return Mapper.Map<OrderDetailsViewModel>(TheUnitOfWork.OrderDetails.GetOrderDetailById(id));
        }



        public bool SaveNewOrderDetail(OrderDetailsViewModel OrderDetailViewModel)
        {
            bool result = false;
            var OrderDetail = Mapper.Map<Order_Product>(OrderDetailViewModel);
            if (TheUnitOfWork.OrderDetails.Insert(OrderDetail))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateOrderDetail(OrderDetailsViewModel OrderDetailViewModel)
        {
            var OrderDetail = Mapper.Map<Order_Product>(OrderDetailViewModel);
            TheUnitOfWork.OrderDetails.Update(OrderDetail);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteOrderDetail(int id)
        {
            TheUnitOfWork.OrderDetails.Delete(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckOrderDetailExists(OrderDetailsViewModel OrderDetailViewModel)
        {
            Order_Product OrderDetail = Mapper.Map<Order_Product>(OrderDetailViewModel);
            return TheUnitOfWork.OrderDetails.CheckOrderDetailExists(OrderDetail);
        }

        public List<Order_Product> GetOrderDetailByOrderId(int Orderid)
        {
            return TheUnitOfWork.OrderDetails.GetOrderDetailByOrderId(Orderid).ToList();
        }
        #endregion
    }
}
