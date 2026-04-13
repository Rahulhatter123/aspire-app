using System.Net.Http.Json;
using System.Text.Json;
using Clean_Architecture_Sample.Application.DTOs;

namespace Clean_Architecture_Sample.ProductIntegrationTest;

public class ProductTest :  IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public ProductTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    [Fact]
    public async Task FullProductLifecycle_Test()
    {
        // 1. CREATE
        var productDto = new ProductDto { Name = "SQL Test Product", Price = 99 };
        var postResponse = await _client.PostAsJsonAsync("https://localhost:7216/api/Product/insert", productDto);
        var created = await postResponse.Content.ReadFromJsonAsync<ProductResponseDto>(_jsonOptions);

        Assert.NotNull(created);
        var newId = created.Id;

        // 2. GET BY ID (Verify it's in the real SQL DB)
        var getResponse = await _client.GetAsync($"/api/product/product-id/{newId}");
        Assert.Equal(System.Net.HttpStatusCode.OK, getResponse.StatusCode);

        // 3. DELETE
        var deleteResponse = await _client.DeleteAsync($"/api/product/delete/{newId}");
        Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task GetProductById_ShouldReturnProduct()
    {
        // Arrange
        var productId = 4; // Assuming a product with ID 1 exists

        // Act
        var response = await _client.GetAsync($"https://localhost:7216/api/product/product-id/{productId}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var product = JsonSerializer.Deserialize<ProductResponseDto>(responseContent, options);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(productId, product.Id);
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnListOfProducts()
    {
        // Act
        var response = await _client.GetAsync("https://localhost:7216/api/product/products");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<ProductResponseDto>>(responseContent);

        // Assert
        Assert.NotNull(products);
        Assert.NotEmpty(products);
    }
}
