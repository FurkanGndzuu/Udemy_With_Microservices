using DiscountService.API.DTOs;
using DiscountService.API.Models.Entities;
using ServicesShared;

namespace DiscountService.API.Abstarctions
{
    public interface IDiscountService
    {
        Task<Response<List<Discount>>> GetAllDiscounts();
        Task<Response<Discount>> GetDiscountsById(int id);
        Task<Response<NoContent>> UpdateDiscount(DiscountDTO discount);
        Task<Response<NoContent>> SaveDiscount(DiscountDTO discount);
        Task<Response<NoContent>> DeleteDiscount(int id);
        

        Task<Response<Discount>> GetDiscountsByCodeAndUserId(string Code , string userId);
    }
}
