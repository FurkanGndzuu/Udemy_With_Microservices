using ClientForWeb.Models;

namespace ClientForWeb.Abstractions
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        Task<List<OrderViewModel>> GetOrder();

    }
}
