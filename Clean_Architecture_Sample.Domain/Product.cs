using System.ComponentModel.DataAnnotations;

namespace Clean_Architecture_Sample.Domain;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string Description { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ImageFile { get; set; }
}
