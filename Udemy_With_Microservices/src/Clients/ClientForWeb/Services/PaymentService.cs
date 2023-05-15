using ClientForWeb.Abstractions;
using ClientForWeb.Models;

namespace ClientForWeb.Services
{
    public class PaymentService : IPaymentService
    {

        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("payments", paymentInfoInput);

            return response.IsSuccessStatusCode;
        }
    }
}
