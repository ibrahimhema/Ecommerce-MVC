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
    public class ProductImagesRepositoty : BaseRepository<ProductImages>
    {
        public ProductImagesRepositoty(DbContext db):base(db)
        {
        }

        #region CRUB

        public List<ProductImages> GetAllProductSize()
        {
            return GetAll().ToList();
        }

        public bool InsertProductImages(ProductImages productImages)
        {
            return Insert(productImages);
        }
        public void UpdateProductImages(ProductImages productImages)
        {
            Update(productImages);
        }
        public void DeleteProductImages(int id)
        {
            Delete(id);
        }

        public bool CheckProductImagesExists(ProductImages productImages)
        {
            return GetAny(b => b.Id == productImages.Id);
        }
        public Brand GetProductImagesId(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        #endregion
    }
}
