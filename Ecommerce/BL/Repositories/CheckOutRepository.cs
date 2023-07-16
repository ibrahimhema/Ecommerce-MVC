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
    public class CheckOutRepository:BaseRepository<Order>
    {
        public CheckOutRepository(DbContext db) : base(db)
        {
        }

        #region CRUB

        public List<Order> GetAllCheckOuts()
        {
            return GetAll().Include(x=>x.User).Include(x=>x.Payment).ToList();
        }

        public bool InsertCheckOut(Order CheckOut)
        {
            return Insert(CheckOut);
        }
        public void UpdateCheckOut(Order CheckOut)
        {
            Update(CheckOut);
        }
        public void DeleteCheckOut(int id)
        {
            Delete(id);
        }

        public bool CheckCheckOutExists(Order CheckOut)
        {
            return GetAny(b => b.Id == CheckOut.Id);
        }
        public Order GetCheckOutById(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        public IQueryable<Order> GetCheckOutByStutes(Order_Status Stutes)
        {
            return GetAll().Where(r => r.Status == Stutes);
        }
        public IQueryable<Order> GetCheckOutByDate(DateTime from,DateTime to)
        {
            return GetAll().Where(r=>r.Created_at >= from && r.Created_at<=to);
        }
        #endregion
    }
}
