using Common.Response;
using MediatR;

namespace StoreProducts.Core.User.Query;

public class UserLoginQuery :IRequest<ServiceResponse<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}