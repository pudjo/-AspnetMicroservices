using BucketAPI.Entities;
using DiscountGRPC.Protos;

namespace BucketAPI.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName);
    Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
    Task DeleteBasket(string userName);
    Task<CouponModel> GetDiscount(string productName);
}
