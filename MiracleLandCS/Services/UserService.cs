using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MiracleLandCS.Models;

namespace MiracleLandCS.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public UserService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<GetAccountInfo?> GetAccountInfoAsync()
        {
            var token = await _authService.GetSavedTokenAsync();
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("User is not logged in.");

            var response = await _httpClient.GetAsync($"/api/CsUserAccounts/GetAccountInfo?token={token}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GetAccountInfo>();
            }
            else
            {
                throw new HttpRequestException($"Failed to fetch account info: {response.ReasonPhrase}");
            }
        }
    }
}
