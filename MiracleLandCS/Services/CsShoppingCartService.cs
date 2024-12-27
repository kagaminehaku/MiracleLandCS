using System.Net.Http.Json;
using MiracleLandCS.Models;

public class CsShoppingCartService
{
    private readonly HttpClient _httpClient;

    public CsShoppingCartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CsProducts>> GetShoppingCartItemsAsync(string token)
    {
        var response = await _httpClient.GetAsync($"/api/CsProducts/GetShoppingCartItems?token={token}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<CsProducts>>() ?? new List<CsProducts>();
        }
        else
        {
            throw new Exception($"Failed to fetch cart items: {response.ReasonPhrase}");
        }
    }
}
