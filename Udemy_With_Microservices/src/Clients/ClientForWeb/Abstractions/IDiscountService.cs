using ClientForWeb.Models;

namespace ClientForWeb.Abstractions
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
