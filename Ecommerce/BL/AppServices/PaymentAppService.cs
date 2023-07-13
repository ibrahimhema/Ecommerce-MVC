using BL.Bases;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
   public class PaymentAppService: BaseAppService
    {
        #region CURD

        public List<Payment> GetAllPayment()
        {
            return Mapper.Map<List<Payment>>(TheUnitOfWork.Payment.GetAllPayment());
        }
        public Payment find(Payment_Method payment)
        {
            return Mapper.Map<Payment>(TheUnitOfWork.Payment.GetAll().FirstOrDefault(e=>e.Payment_Method==payment));
        }



        public bool SaveNewPayment(Payment Payment)
        {
            bool result = false;
            var payment = Mapper.Map<Payment>(Payment);
            if (TheUnitOfWork.Payment.Insert(payment))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdatePayment(Payment Payment)
        {
            var payment = Mapper.Map<Payment>(Payment);
            TheUnitOfWork.Payment.Update(payment);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeletePayment(int id)
        {
            TheUnitOfWork.Payment.Delete(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckPaymentExists(Payment Payment)
        {
            Payment payment = Mapper.Map<Payment>(Payment);
            return TheUnitOfWork.Payment.CheckPaymentExists(payment);
        }
        #endregion
    }
}
