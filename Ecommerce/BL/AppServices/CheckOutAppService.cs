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
   public  class CheckOutAppService:BaseAppService
    {
        #region CURD

        public List<CheckOutViewModel> GetAllCheckOut()
        {
            return Mapper.Map<List<CheckOutViewModel>>(TheUnitOfWork.CheckOut.GetAllCheckOuts());
        }
        public CheckOutViewModel GetCheckOut(int id)
        {
            return Mapper.Map<CheckOutViewModel>(TheUnitOfWork.CheckOut.GetCheckOutById(id));
        }

        public Order GetCheckOut_ByModel(int id)
        {
            return TheUnitOfWork.CheckOut.GetCheckOutById(id);
        }

        public List<CheckOutViewModel> GetUserOrders(string id)
        {
            return Mapper.Map<List<CheckOutViewModel>>(TheUnitOfWork.CheckOut.GetWhere(o => o.User_Id == id));
        }



        public int SaveNewCheckOut(CheckOutViewModel CheckOutViewModel)
        {
            bool result = false;
            var CheckOut = Mapper.Map<Order>(CheckOutViewModel);
            int id = 0;
            if (TheUnitOfWork.CheckOut.Insert(CheckOut))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            if(result)
            return CheckOut.Id;
            else
            {
                return id;
            }
        }


        public bool UpdateCheckOut(CheckOutViewModel CheckOutViewModel)
        {
            var CheckOut = Mapper.Map<Order>(CheckOutViewModel);
            TheUnitOfWork.CheckOut.Update(CheckOut);
            TheUnitOfWork.Commit();

            return true;
        }

        public bool UpdateCheckOut_ByModel(Order orderModel)
        {
            var CheckOut = orderModel;
            TheUnitOfWork.CheckOut.Update(CheckOut);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteCheckOut(int id)
        {
            TheUnitOfWork.CheckOut.Delete(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckCheckOutExists(CheckOutViewModel CheckOutViewModel)
        {
            Order CheckOut = Mapper.Map<Order>(CheckOutViewModel);
            return TheUnitOfWork.CheckOut.CheckCheckOutExists(CheckOut);
        }
        public List<CheckOutViewModel> GetCheckOutByStutes(Order_Status Stutes)
        {
            
            return Mapper.Map<List<CheckOutViewModel>>(TheUnitOfWork.CheckOut.GetCheckOutByStutes(Stutes));
        }
        public List<CheckOutViewModel> GetCheckOutByDate(DateTime from, DateTime to)
        {
           
            return Mapper.Map<List<CheckOutViewModel>>(TheUnitOfWork.CheckOut.GetCheckOutByDate(from, to));
        }
        #endregion
    }
}
