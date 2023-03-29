using AutoMapper;
using Basket.API1.Entities;
using EventBus.Messages.Events;

namespace Basket.API1.Mapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}

