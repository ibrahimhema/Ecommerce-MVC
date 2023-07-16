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
    public class ProductSizeRepositoty : BaseRepository<ProductSizes>
    {
        public ProductSizeRepositoty(DbContext db):base(db)
        {
        }

        #region CRUB

        public List<ProductSizes> GetAllProductSize()
        {
            return GetAll().ToList();
        }
        public List<ProductSizes> GetAllProductSizeByProductId(int productId)
        {
            return GetAll().Where(x=>x.ProductId==productId).ToList();
        }
        public bool InsertProductSize(ProductSizes productSize)
        {
            return Insert(productSize);
        }
        public void UpdateProductSize(ProductSizes productSize)
        {
            Update(productSize);
        }
        public void DeleteProductSize(int id)
        {
            Delete(id);
        }

        public bool CheckProductSizeExists(ProductSizes productSize)
        {
            return GetAny(b => b.Id == productSize.Id);
        }
        public ProductSizes GetProductSizeId(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        #endregion
    }
}
