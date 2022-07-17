using AutoMapper;
using BucketAPI.Entities;
using EventBus.Messages.Events;

namespace BucketAPI.Mapper;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
    }
}
