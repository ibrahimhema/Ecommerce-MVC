using BL.Bases;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
   public class WalletAppService : BaseAppService
    {
        #region CURD

        public List<Wallet> GetAllWallet()
        {
            return TheUnitOfWork.Wallet.GetAllWallets();
        }
        public Wallet GetWallet(int id)
        {
            return TheUnitOfWork.Wallet.GetWaletById(id);
        }



        public bool SaveNewWallet(Wallet wallet)
        {
            bool result = false;
           
            if (TheUnitOfWork.Wallet.Insert(wallet))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateWallet(Wallet wallet)
        {
         
            TheUnitOfWork.Wallet.Update(wallet);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteWallet(int id)
        {
            TheUnitOfWork.Wallet.Delete(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckWalletExists(Wallet wallet)
        {
            
            return TheUnitOfWork.Wallet.CheckWalletExists(wallet);
        }
        public Wallet GetWalletByUserId(string id)
        {

            return TheUnitOfWork.Wallet.GetWaletByUserId(id);
        }
        #endregion
    }
}
