using FluentValidation;
using StoreProducts.Core.Product.Command;

namespace StoreProducts.Infrastructure.Validation.Product.Update;

public class UpdateProductCommandValidator :AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).Must(q => q > 0)
            .WithMessage("Please enter a valid Id");

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MinimumLength(3);
        RuleFor(x => x.ManufactureEmail).EmailAddress();
    }
}