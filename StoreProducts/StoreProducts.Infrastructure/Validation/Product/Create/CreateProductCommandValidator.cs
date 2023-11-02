using FluentValidation;
using StoreProducts.Core.Product.Command;

namespace StoreProducts.Infrastructure.Validation.Product.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MinimumLength(3);
        RuleFor(x => x.ManufactureEmail).EmailAddress();

        RuleFor(x => x.ProduceDate).
            Must(date => date != default)
            .WithMessage("ProduceDate is required"); ;

    }

}