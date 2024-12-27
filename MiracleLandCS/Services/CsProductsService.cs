using MiracleLandCS.Models;
using System.Net.Http.Json;

public class CsProductsService
{
    private readonly HttpClient _httpClient;

    public CsProductsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CsProducts>> GetAllProductsAsync()
    {
        var response = await _httpClient.GetAsync("api/CsProducts");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CsProducts>>() ?? new List<CsProducts>();
    }

    public async Task<CsProducts> GetProductByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/CsProducts/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CsProducts>() ?? new CsProducts();
    }

    public async Task<string> UpdateShoppingCartAsync(CsProductsToCart productToCart)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/CsProducts/UpdateShoppingCart", productToCart);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return $"Failed to update cart: {response.ReasonPhrase}";
    }
}
