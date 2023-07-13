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
    public class ProductSizeRepositoty : BaseRepository<ProductSize>
    {
        public ProductSizeRepositoty(DbContext db):base(db)
        {
        }

        #region CRUB

        public List<ProductSize> GetAllProductSize()
        {
            return GetAll().ToList();
        }

        public bool InsertProductSize(ProductSize productSize)
        {
            return Insert(productSize);
        }
        public void UpdateProductSize(ProductSize productSize)
        {
            Update(productSize);
        }
        public void DeleteProductSize(int id)
        {
            Delete(id);
        }

        public bool CheckProductSizeExists(ProductSize productSize)
        {
            return GetAny(b => b.Id == productSize.Id);
        }
        public Brand GetProductSizeId(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        #endregion
    }
}
