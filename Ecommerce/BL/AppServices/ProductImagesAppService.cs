using BL.Bases;
using BL.ViewModels;

using System.Collections.Generic;


namespace BL.AppServices
{
    public class ProductImagesAppService : BaseAppService
    {
        #region CURD
       
        public List<ProductImagesDTO> GetAllProductSize()
        {
            return Mapper.Map<List<ProductImagesDTO>>(TheUnitOfWork.ProductImagesRepositoty.GetAllProductSize());
        }
        public ProductImagesDTO GetProductImages(int id)
        {
            return Mapper.Map<ProductImagesDTO>(TheUnitOfWork.ProductImagesRepositoty.GetProductImagesId(id));
        }



        public bool SaveNewProductImages(ProductImagesDTO productImagesDTO)
        {
            bool result = false;
            var brand = Mapper.Map<ProductImages>(productImagesDTO);
            if (TheUnitOfWork.ProductImagesRepositoty.Insert(brand))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateProductSize(ProductImagesDTO productImagesDTO)
        {
            var Brand = Mapper.Map<ProductImages>(productImagesDTO);
            TheUnitOfWork.ProductImagesRepositoty.Update(Brand);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteProductImages(int id)
        {
            TheUnitOfWork.ProductImagesRepositoty.Delete(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckProductSizeExists(ProductImagesDTO productImagesDTO)
        {
            ProductImages Brand = Mapper.Map<ProductImages>(productSizeDTO);
            return TheUnitOfWork.ProductImagesRepositoty.CheckProductImagesExists(Brand);
        }
        #endregion
    }
}
