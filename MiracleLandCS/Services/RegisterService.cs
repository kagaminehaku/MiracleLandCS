using System.Net.Http.Json;
using MiracleLandCS.Models;

namespace MiracleLandCS.Services
{
    public class RegisterService
    {
        private readonly HttpClient _httpClient;

        public RegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> RegisterUserAsync(UserRegisterRequest registerRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/CsUserAccounts/Register", registerRequest);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return $"Registration failed: {errorMessage}";
                }

                return "Registration successful.";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }
    }
}
