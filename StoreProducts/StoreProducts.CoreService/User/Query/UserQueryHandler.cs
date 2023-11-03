using Common.BaseService;
using Common.Response;
using MediatR;
using StoreProducts.Core.User.Query;
using StoreProducts.Core.User.RepositoryQuery;

namespace StoreProducts.CoreService.User.Query;

public class UserQueryHandler :BaseService,IRequestHandler<UserLoginQuery,ServiceResponse<string>>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public UserQueryHandler(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }

    public async Task<ServiceResponse<string>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
    {
        return await _userQueryRepository.Login(request);
    }
}