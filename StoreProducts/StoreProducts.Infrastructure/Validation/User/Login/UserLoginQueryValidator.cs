using FluentValidation;
using StoreProducts.Core.User.Query;

namespace StoreProducts.Infrastructure.Validation.User.Login
{
    public class UserLoginQueryValidator:AbstractValidator<UserLoginQuery>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
