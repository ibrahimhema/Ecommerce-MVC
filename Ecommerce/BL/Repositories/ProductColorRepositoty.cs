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
    public class ProductColorRepositoty : BaseRepository<ProductColors>
    {
        public ProductColorRepositoty(DbContext db):base(db)
        {
        }

        #region CRUB

        public List<ProductColors> GetAllProductColor()
        {
            return GetAll().ToList();
        }

        public bool InsertProductColor(ProductColors productColor)
        {
            return Insert(productColor);
        }
        public void UpdateProductColor(ProductColors productColor)
        {
            Update(productColor);
        }
        public void DeleteroductColor(int id)
        {
            Delete(id);
        }

        public bool CheckProductColorExists(ProductColors productColor)
        {
            return GetAny(b => b.Id == productColor.Id);
        }
        public ProductColors GetproductColorId(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        #endregion
    }
}
