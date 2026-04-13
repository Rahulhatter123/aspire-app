using Clean_Architecture_Sample.Application.Features.Product.Commands;
using FluentValidation;

namespace Clean_Architecture_Sample.Application.Features.Product.Validations;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product id must be greater than 0.");

        RuleFor(x => x.Product)
            .NotNull().WithMessage("Product payload is required.")
            .SetValidator(new ProductDtoValidator());
    }
}
