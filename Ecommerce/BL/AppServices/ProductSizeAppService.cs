using BL.Bases;
using BL.ViewModels;

using System.Collections.Generic;


namespace BL.AppServices
{
    public class ProductSizeAppService : BaseAppService
    {
        #region CURD
       
        public List<ProductSizeDTO> GetAllProductSize()
        {
            return Mapper.Map<List<ProductSizeDTO>>(TheUnitOfWork.ProductSizeRepositoty.GetAllProductSize());
        }
        public ProductSizeDTO GetProductSize(int id)
        {
            return Mapper.Map<ProductSizeDTO>(TheUnitOfWork.ProductSizeRepositoty.GetProductSizeId(id));
        }



        public bool SaveNewProductSize(ProductSizeDTO productSizeDTO)
        {
            bool result = false;
            var brand = Mapper.Map<ProductSize>(productSizeDTO);
            if (TheUnitOfWork.ProductSizeRepositoty.Insert(brand))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateProductSize(ProductSizeDTO productSizeDTO)
        {
            var Brand = Mapper.Map<ProductSize>(productSizeDTO);
            TheUnitOfWork.ProductSizeRepositoty.Update(Brand);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteProductSize(int id)
        {
            TheUnitOfWork.ProductSizeRepositoty.Delete(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckProductSizeExists(ProductSizeDTO productSizeDTO)
        {
            ProductSize Brand = Mapper.Map<ProductSize>(productSizeDTO);
            return TheUnitOfWork.ProductSizeRepositoty.CheckProductSizeExists(Brand);
        }
        #endregion
    }
}
