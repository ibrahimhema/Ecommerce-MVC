using AutoMapper;
using BL.ViewModels;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Configurations
{
    static class MapperConfig
    {
        public static IMapper Mapper { get; set; }

        static MapperConfig()
        {
            var conig = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<ApplicationUser, LoginViewModel>().ReverseMap();
                    cfg.CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
                    cfg.CreateMap<ApplicationUser, AdminDisplayUserViewModel>().ReverseMap();
                    cfg.CreateMap<ApplicationUser, ProfileViewModel>().ReverseMap();
                    cfg.CreateMap<ApplicationUser, ProfileEditViewModel>().ReverseMap();
                    cfg.CreateMap<Product, ProductViewModel>().ReverseMap();
                    cfg.CreateMap<Brand, BrandViewModel>().ReverseMap();
                    cfg.CreateMap<Product, ProductViewModel>().ReverseMap();
                    cfg.CreateMap<Rating, RatingViewModel>().ReverseMap();
                    cfg.CreateMap<Order, CheckOutViewModel>().ReverseMap();
                    cfg.CreateMap<Order_Product, OrderDetailsViewModel>().ReverseMap();
                    cfg.CreateMap<WishList, WishListViewModel>().ReverseMap();
                    cfg.CreateMap<Sub_Category, SubCategoryViewModel>().ReverseMap();


                    cfg.CreateMap<ProductSizes , ProductSizeDTO>().ReverseMap();
                    cfg.CreateMap<ProductColors, ProductColorDTO>().ReverseMap();
                    cfg.CreateMap<ProductImages, ProductImagesDTO>().ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.PhotoURL)).ReverseMap();


                    cfg.CreateMap<Main_Category, MainCategoryViewModelForAjax>().ReverseMap();

                });

            Mapper = conig.CreateMapper();
        }
    }
}
