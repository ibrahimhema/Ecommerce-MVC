using BL.Bases;
using BL.ViewModels;
using DAL.Model;
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
        public ProductSizes GetProductSize(int id)
        {
            return TheUnitOfWork.ProductSizeRepositoty.GetProductSizeId(id);
        }

        public List<ProductSizeDTO> GetProductSizeByProductId(int ProductId)
        {
            return Mapper.Map<List<ProductSizeDTO>>(TheUnitOfWork.ProductSizeRepositoty.GetAllProductSizeByProductId(ProductId));
        }


        public bool SaveNewProductSize(ProductSizeDTO productSizeDTO)
        {
            bool result = false;
            var brand = Mapper.Map<ProductSizes>(productSizeDTO);
            if (TheUnitOfWork.ProductSizeRepositoty.Insert(brand))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateProductSize(ProductSizes productSize)
        {
            //var Brand = Mapper.Map<ProductSizes>(productSizeDTO);
            TheUnitOfWork.ProductSizeRepositoty.Update(productSize);
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
            ProductSizes Brand = Mapper.Map<ProductSizes>(productSizeDTO);
            return TheUnitOfWork.ProductSizeRepositoty.CheckProductSizeExists(Brand);
        }
        #endregion
    }
}
