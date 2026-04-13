using Clean_Architecture_Sample.Application.DTOs;
using FluentValidation;

namespace Clean_Architecture_Sample.Application.Features.Product.Validations;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    private const int MaxNameLength = 200;
    private const int MaxDescriptionLength = 2000;
    private const long MaxImageSizeInBytes = 5 * 1024 * 1024; // 5 MB

    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp"];

    public ProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(MaxNameLength).WithMessage($"Product name must not exceed {MaxNameLength} characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Product quantity cannot be negative.");

        RuleFor(x => x.Description)
            .MaximumLength(MaxDescriptionLength)
            .WithMessage($"Product description must not exceed {MaxDescriptionLength} characters.");

        When(x => x.ImageFile is not null, () =>
        {
            RuleFor(x => x.ImageFile!.Length)
                .GreaterThan(0).WithMessage("Product image file must not be empty.")
                .LessThanOrEqualTo(MaxImageSizeInBytes)
                .WithMessage($"Product image file size must not exceed {MaxImageSizeInBytes / (1024 * 1024)} MB.");

            RuleFor(x => x.ImageFile!.FileName)
                .Must(HaveAllowedImageExtension)
                .WithMessage("Product image format is invalid. Allowed formats: .jpg, .jpeg, .png, .webp.");
        });
    }

    private static bool HaveAllowedImageExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return AllowedImageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
    }
}
