using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MiracleLandCS.Models;

namespace MiracleLandCS.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private const string TokenKey = "auth_token";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var loginRequest = new UserLoginRequest
            {
                username = username,
                password = password
            };

            var response = await _httpClient.PostAsJsonAsync("api/CsUserAccounts/CsLogin", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseBody = await response.Content.ReadFromJsonAsync<JsonElement>();
            var token = responseBody.GetProperty("token").GetString();

            // Save token securely
            if (!string.IsNullOrEmpty(token))
            {
                await SecureStorage.SetAsync(TokenKey, token);
            }

            return token;
        }

        public async Task<string?> GetSavedTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync(TokenKey);
            }
            catch
            {
                return null;
            }
        }

        public async Task ClearTokenAsync()
        {
            try
            {
                SecureStorage.Remove(TokenKey);
            }
            catch
            {
                Console.WriteLine("Secure storage not available on device.");
            }
        }

        public async Task<bool> IsTokenValidAsync()
        {
            var token = await GetSavedTokenAsync();
            if (string.IsNullOrEmpty(token)) return false;

            // Add logic to validate token (e.g., check expiration by decoding JWT or API validation)
            var response = await _httpClient.GetAsync($"api/CsUserAccounts/ValidateToken?token={token}");
            return response.IsSuccessStatusCode;
        }
    }
}
