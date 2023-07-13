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
   public class PaymentRepository: BaseRepository<Payment>
    {
        public PaymentRepository(DbContext db) : base(db)
        {

        }

        public List<Payment> GetAllPayment()
        {
            return GetAll().ToList();
        }

        public bool InsertPayment(Payment Payment)
        {
            return Insert(Payment);
        }
        public void UpdatePayment(Payment Payment)
        {
            Update(Payment);
        }
        public void DeletePayment(int id)
        {
            Delete(id);
        }

        public bool CheckPaymentExists(Payment Payment)
        {
            return GetAny(b => b.Id == Payment.Id);
        }
        public Payment GetPaymentById(Payment_Method payment)
        {
            return GetFirstOrDefault(b => b.Payment_Method == payment);
        }
    }
}
