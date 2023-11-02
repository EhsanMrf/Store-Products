using Common.BaseService;
using Common.Response;
using MediatR;
using StoreProducts.Core.User.Command;
using StoreProducts.Core.User.RepositoryCommand;

namespace StoreProducts.CoreService.User.Command;

public class UserRegisterCommandHandler : BaseService,IRequestHandler<UserRegisterCommand,ServiceResponse<bool>>
{
    private readonly IUserRegisterRepository _userRegisterRepository;

    public UserRegisterCommandHandler(IUserRegisterRepository userRegisterRepository)
    {
        _userRegisterRepository = userRegisterRepository;
    }

    public async Task<ServiceResponse<bool>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        return await _userRegisterRepository.UserRegister(request);
    }
}