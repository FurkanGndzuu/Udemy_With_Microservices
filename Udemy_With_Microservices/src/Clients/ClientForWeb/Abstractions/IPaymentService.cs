using ClientForWeb.Models;

namespace ClientForWeb.Abstractions
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
