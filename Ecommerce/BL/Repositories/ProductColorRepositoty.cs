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
    public class ProductColorRepositoty : BaseRepository<ProductColor>
    {
        public ProductColorRepositoty(DbContext db):base(db)
        {
        }

        #region CRUB

        public List<ProductColor> GetAllProductColor()
        {
            return GetAll().ToList();
        }

        public bool InsertProductColor(ProductColor productColor)
        {
            return Insert(productColor);
        }
        public void UpdateProductColor(ProductColor productColor)
        {
            Update(productColor);
        }
        public void DeleteroductColor(int id)
        {
            Delete(id);
        }

        public bool CheckProductColorExists(ProductColor productColor)
        {
            return GetAny(b => b.Id == productColor.Id);
        }
        public ProductColor GetproductColorId(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        #endregion
    }
}
