using Common.Response;
using MediatR;

namespace StoreProducts.Core.User.Command;

public class UserRegisterCommand :IRequest<ServiceResponse<bool>>
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}