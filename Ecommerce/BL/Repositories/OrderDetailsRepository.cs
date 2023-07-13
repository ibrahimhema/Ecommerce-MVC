using BL.Bases;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
   public class OrderDetailsRepository: BaseRepository<Order_Product>
    {
        public OrderDetailsRepository(DbContext db) : base(db)
        {
        }

        #region CRUB

        public List<Order_Product> GetAllOrderDetails()
        {
            return GetAll().ToList();
        }

        public bool InsertOrderDetail(Order_Product OrderDetail)
        {
            return Insert(OrderDetail);
        }
        public void UpdateOrderDetail(Order_Product OrderDetail)
        {
            Update(OrderDetail);
        }
        public void DeleteOrderDetail(int id)
        {
            Delete(id);
        }

        public bool CheckOrderDetailExists(Order_Product OrderDetail)
        {
            return GetAny(b => b.Id == OrderDetail.Id);
        }
        public Order_Product GetOrderDetailById(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        public IQueryable<Order_Product> GetOrderDetailByOrderId(int Orderid)
        {
            return GetAll().Where(b => b.Order_Id == Orderid);
        }
        #endregion
    }
}
