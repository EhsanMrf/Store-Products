using FluentValidation;
using StoreProducts.Core.User.Command;

namespace StoreProducts.Infrastructure.Validation.User.Register;

public class UserRegisterCommandValidator :AbstractValidator<UserRegisterCommand>
{
    public UserRegisterCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.FullName).MinimumLength(4);

        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Password).MinimumLength(5);
        
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password);
    }
}