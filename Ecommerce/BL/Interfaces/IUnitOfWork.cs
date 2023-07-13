using BL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        AccountRepository Account { get; }
        RoleRepository Role { get; }
        //OrderRepository Order { get; }
        RatingRepository Rating { get; }

        BrandRepositoty Brand { get; }
        Main_CatRepository MainCategory { get; }

        ProductRepository Product { get; }

        SubCategoryRespository SubCategory { get; }

        WishListRepository WishList { get; }
       CheckOutRepository CheckOut { get; }
        OrderDetailsRepository OrderDetails { get; }
        PaymentRepository Payment { get; }
        WalletReposatory Wallet { get;}
        NotificationReposatory Notification { get; }

        int Commit();
    }
}
