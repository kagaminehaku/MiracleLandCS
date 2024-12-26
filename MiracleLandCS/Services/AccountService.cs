using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using MiracleLandCS.Models;
using MiracleLandCS.Services;

public class AccountService
{
    private readonly HttpClient _httpClient;
    private AuthService _authService;

    public AccountService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<GetAccountInfo?> GetAccountInfoAsync()
    {
        var token = await _authService.GetSavedTokenAsync();
        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("User is not logged in.");
        }

        var response = await _httpClient.GetAsync($"api/CsUserAccounts/GetAccountInfo?token={token}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GetAccountInfo>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<string> UpdateAccountInfoAsync(UserAccountUpdate userAccountUpdate)
    {
        var token = await _authService.GetSavedTokenAsync();
        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("User is not logged in.");
        }

        userAccountUpdate.token = token;

        var content = new MultipartFormDataContent
    {
        { new StringContent(userAccountUpdate.token), "token" }
    };
        if (userAccountUpdate.Email != null) content.Add(new StringContent(userAccountUpdate.Email), "Email");
        if (userAccountUpdate.Phone != null) content.Add(new StringContent(userAccountUpdate.Phone), "Phone");
        if (userAccountUpdate.Address != null) content.Add(new StringContent(userAccountUpdate.Address), "Address");
        if (!string.IsNullOrEmpty(userAccountUpdate.AvatarContent))
        {
            content.Add(new StringContent(userAccountUpdate.AvatarContent), "AvatarContent");
        }

        var response = await _httpClient.PutAsync("api/CsUserAccounts/UpdateUserInfo", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

}
