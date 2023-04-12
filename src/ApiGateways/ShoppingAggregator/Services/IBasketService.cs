using Shoppingaggregator.Models;

namespace Shoppingaggregator.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}