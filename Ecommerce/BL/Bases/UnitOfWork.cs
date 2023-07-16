using BL.Interfaces;
using BL.Repositories;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Shared Props
        private DbContext EC_Context { get; set; }
        #endregion


        #region Props and Data Fields
        private AccountRepository account;
        public AccountRepository Account
        {
            get
            {
                account = account ?? new AccountRepository(EC_Context);
                return account;
            }
        }

        private RoleRepository role;
        public RoleRepository Role
        {
            get
            {
                role = role ?? new RoleRepository(EC_Context);
                return role;
            }
        }

        private BrandRepositoty brand;
        public BrandRepositoty Brand
        {
            get
            {
                brand = brand ?? new BrandRepositoty(EC_Context);
                return brand;
            }
        }

        public ProductRepository product;
        public ProductRepository Product
        {
            get
            {
                product = product ?? new ProductRepository(EC_Context);
                return product;
            }
        }

        private Main_CatRepository mainCategory;
        public Main_CatRepository MainCategory
        {
            get
            {
                mainCategory = mainCategory ?? new Main_CatRepository(EC_Context);
                return mainCategory;
            }
        }
        
        private SubCategoryRespository subCategory;
        public SubCategoryRespository SubCategory
        {
            get
            {
                subCategory = subCategory ?? new SubCategoryRespository(EC_Context);
                return subCategory;
            }
        }

        private WishListRepository wishList;
        public WishListRepository WishList
        {
            get
            {
                wishList = wishList ?? new WishListRepository(EC_Context);
                return wishList;
            }
        }
        private RatingRepository rating;
        public RatingRepository Rating
        {
            get
            {
                rating = rating ?? new RatingRepository(EC_Context);
                return rating;
            }
        }
        private CheckOutRepository checkOut;
        public CheckOutRepository CheckOut
        {
            get
            {
                checkOut = checkOut ?? new CheckOutRepository(EC_Context);
                return checkOut;
            }
        }
        private OrderDetailsRepository orderDetails;
        public OrderDetailsRepository OrderDetails
        {
            get
            {
                orderDetails = orderDetails ?? new OrderDetailsRepository(EC_Context);
                return orderDetails;
            }
        }
        private PaymentRepository payment;
        public PaymentRepository Payment
        {
            get
            {
                payment = payment ?? new PaymentRepository(EC_Context);
                return payment;
            }
        }
        public WalletReposatory wallet;
        public WalletReposatory Wallet
        {
            get
            {
                wallet = wallet ?? new WalletReposatory(EC_Context);
                return wallet;
            }
        }
        private NotificationReposatory notification;
        public NotificationReposatory Notification
        {
            get
            {
              return notification = notification ?? new NotificationReposatory(EC_Context);
            }
        }
        private ProductImagesRepositoty productImagesRepositoty;
        public ProductImagesRepositoty ProductImagesRepositoty
        {
            get
            {
                return productImagesRepositoty = productImagesRepositoty ?? new ProductImagesRepositoty(EC_Context);
            }
        }
        private ProductSizeRepositoty productSizeRepositoty;
        public ProductSizeRepositoty ProductSizeRepositoty
        {
            get
            {
                return productSizeRepositoty = productSizeRepositoty ?? new ProductSizeRepositoty(EC_Context);
            }
        }
        private ProductColorRepositoty productColorRepositoty;
        public ProductColorRepositoty ProductColorRepositoty
        {
            get
            {
                return productColorRepositoty = productColorRepositoty ?? new ProductColorRepositoty(EC_Context);
            }
        }

        // Main_CatRepository IUnitOfWork.mainCategory => throw new NotImplementedException();
        #endregion

        #region CTOR

        public UnitOfWork()
        {
            EC_Context = new ApplicationDbContext();
            //EC_Context.Configuration.LazyLoadingEnabled = false;
        }
        #endregion


        #region Methods

        public int Commit()
        {
            return EC_Context.SaveChanges();
        }

        public void Dispose()
        {
            EC_Context.Dispose();
        }
        #endregion
    }
}
