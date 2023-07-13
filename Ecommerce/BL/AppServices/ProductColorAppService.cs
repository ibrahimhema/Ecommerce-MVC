using BL.Bases;
using BL.ViewModels;

using System.Collections.Generic;


namespace BL.AppServices
{
    public class ProductColorAppService : BaseAppService
    {
        #region CURD
       
        public List<ProductColorDTO> GetAllProductSize()
        {
            return Mapper.Map<List<ProductColorDTO>>(TheUnitOfWork.ProductColorRepositoty.GetAllProductColor());
        }
        public ProductColorDTO GetProductSize(int id)
        {
            return Mapper.Map<ProductColorDTO>(TheUnitOfWork.ProductColorRepositoty.GetproductColorId(id));
        }



        public bool SaveNewProductColor(ProductColorDTO productColorDTO)
        {
            bool result = false;
            var brand = Mapper.Map<ProductColor>(productColorDTO);
            if (TheUnitOfWork.ProductColorRepositoty.Insert(brand))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateproductColor(ProductColorDTO productColorDTO)
        {
            var Brand = Mapper.Map<ProductColor>(productColorDTO);
            TheUnitOfWork.ProductColorRepositoty.Update(Brand);
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
            ProductColor Brand = Mapper.Map<ProductColor>(productColorDTO);
            return TheUnitOfWork.ProductColorRepositoty.CheckProductColorExists(Brand);
        }
        #endregion
    }
}
