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
    public class WalletReposatory : BaseRepository<Wallet>
    {
        public WalletReposatory(DbContext db) : base(db)
        {
        }
        #region CRUB

        public List<Wallet> GetAllWallets()
        {
            return GetAll().ToList();
        }

        public bool InsertWallet(Wallet wallet)
        {
            return Insert(wallet);
        }
        public void UpdateWallet(Wallet wallet)
        {
            Update(wallet);
        }
        public void DeleteWallet(int id)
        {
            Delete(id);
        }

        public bool CheckWalletExists(Wallet wallet)
        {
            return GetAny(b => b.Id == wallet.Id);
        }
        public Wallet GetWaletById(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        public Wallet GetWaletByUserId(string id)
        {
            return GetFirstOrDefault(b => b.User_Id == id);
        }
        #endregion
    }
}
