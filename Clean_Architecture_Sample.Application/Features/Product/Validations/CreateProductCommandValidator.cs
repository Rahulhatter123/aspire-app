using Clean_Architecture_Sample.Application.Features.Product.Commands;
using FluentValidation;

namespace Clean_Architecture_Sample.Application.Features.Product.Validations;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product)
            .NotNull().WithMessage("Product payload is required.")
            .SetValidator(new ProductDtoValidator());
    }
}
