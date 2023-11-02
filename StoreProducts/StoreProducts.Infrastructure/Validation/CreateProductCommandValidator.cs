using FluentValidation;
using StoreProducts.Core.Product.Command;

namespace StoreProducts.Infrastructure.Validation;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x=>x.Name).NotEmpty();
        RuleFor(x=>x.Name).Length(3);
    }
}