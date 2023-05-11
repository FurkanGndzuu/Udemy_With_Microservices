using BasketService.API.DTOs;
using ServicesShared;

namespace BasketService.API.Abstractions
{
    public interface IBasketService
    {
        Task<Response<BasketDTO>> GetBasket(string userId);
        Task<Response<NoContent>> SaveOrUpdate(BasketDTO basket);
        Task<Response<NoContent>> Delete(string userId);
    }
}
