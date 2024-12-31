using System.Net.Http.Json;
using MiracleLandCS.Models;

namespace MiracleLandCS.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PostOrderAsync(CsOrdersRequest orderRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/CsOrders", orderRequest);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return $"Failed to post order: {response.ReasonPhrase}";
        }

        public async Task<string> GetPaymentURL(PaymentRequest paymentrequest)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/CsProducts/GetPaymentURL", paymentrequest);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return $"Failed to post order: {response.ReasonPhrase}";
        }

        public async Task<List<CsOrdersRequest>> GetUserOrdersAsync(string token)
        {
            return await _httpClient.GetFromJsonAsync<List<CsOrdersRequest>>($"/api/CsOrders/Orders?token={token}");
        }

        public async Task<List<CsOrderDetailRequest>> GetOrderDetailsAsync(Guid orderId)
        {
            return await _httpClient.GetFromJsonAsync<List<CsOrderDetailRequest>>($"/api/CsOrders/order-details/{orderId}");
        }
    }
}
