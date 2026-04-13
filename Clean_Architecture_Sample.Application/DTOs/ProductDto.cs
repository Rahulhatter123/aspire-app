using Microsoft.AspNetCore.Http;

namespace Clean_Architecture_Sample.Application.DTOs;

public class ProductDto
{
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string Description { get; set; } = string.Empty;

    public IFormFile? ImageFile { get; set; }
}