using FluentValidation;
using StoreProducts.Core.Product.Command;

namespace StoreProducts.Infrastructure.Validation.Product.Delete;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).Must(q=>q>0)
            .WithMessage("Please enter a valid Id");
    }
}