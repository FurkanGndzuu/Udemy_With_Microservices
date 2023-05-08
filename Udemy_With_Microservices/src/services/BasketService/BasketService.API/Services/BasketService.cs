using BasketService.API.Abstractions;
using BasketService.API.DTOs;
using ServicesShared;
using System.Text.Json;

namespace BasketService.API.Services
{
    public class BasketService : IBasketService
    {
        readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<NoContent>> Delete(string userId)
        {
            string[] values = userId.Split(' ');
            var status = await _redisService.GetDb().KeyDeleteAsync(values[1]);
            return status ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Basket not found", StatusCodes.Status404NotFound);
        }

        public async Task<Response<BasketDTO>> GetBasket(string userId)
        {
            string[] values = userId.Split(' ');
            var existBasket = _redisService.GetDb().StringGet(values[1]);

            if (String.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDTO>.Fail("Basket is not found", StatusCodes.Status404NotFound);
            }

            return Response<BasketDTO>.Success(JsonSerializer.Deserialize<BasketDTO>(existBasket), StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> SaveOrUpdate(BasketDTO basket)
        {
            var status = await _redisService.GetDb().StringSetAsync(basket.userId, JsonSerializer.Serialize(basket));

            return status ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Basket could not update or save", 500);
        }
    }
}
