using BL.Bases;
using BL.ViewModels;
using DAL.Model;
using System.Collections.Generic;


namespace BL.AppServices
{
    public class ProductColorAppService : BaseAppService
    {
        #region CURD
       
        public List<ProductColorDTO> GetAllProductColor()
        {
            return Mapper.Map<List<ProductColorDTO>>(TheUnitOfWork.ProductColorRepositoty.GetAllProductColor());
        }
        public ProductColors GetProductColor(int id)
        {
            return TheUnitOfWork.ProductColorRepositoty.GetproductColorId(id);
        }



        public bool SaveNewProductColor(ProductColorDTO productColorDTO)
        {
            bool result = false;
            var brand = Mapper.Map<ProductColors>(productColorDTO);
            if (TheUnitOfWork.ProductColorRepositoty.Insert(brand))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateproductColor(ProductColors productColorDTO)
        {
            ;
            TheUnitOfWork.ProductColorRepositoty.Update(productColorDTO);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteProductColor(int id)
        {
            TheUnitOfWork.ProductColorRepositoty.Delete(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckProductColorExists(ProductColorDTO productColorDTO)
        {
            ProductColors Brand = Mapper.Map<ProductColors>(productColorDTO);
            return TheUnitOfWork.ProductColorRepositoty.CheckProductColorExists(Brand);
        }
        #endregion
    }
}
