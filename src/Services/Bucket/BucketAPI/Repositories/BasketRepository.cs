using BucketAPI.Entities;
using DiscountGRPC.Protos;
using Grpc.Net.Client;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BucketAPI.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;
    private readonly IConfiguration _configuration;
    
    public BasketRepository(IConfiguration configuration, IDistributedCache redisCache)
    {
        _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        _configuration = configuration;

    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);

        if (String.IsNullOrEmpty(basket))
            return null;

        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

        return await GetBasket(basket.UserName);
    }

    public async Task DeleteBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }
    public async Task<CouponModel> GetDiscount(string productName)
    {

        string address = _configuration.GetSection("DatabaseSettings").GetSection("GRPCAddress").Value;

        using var channel = GrpcChannel.ForAddress(address);
        var client = new DiscountProtoService.DiscountProtoServiceClient(channel);
        var discountRequest = new GetDiscountRequest { ProductName = productName };

        return await client.GetDiscountAsync(discountRequest);

    }

}