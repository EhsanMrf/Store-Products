using Common.TransientService;
using StoreProducts.Core.User.Command;

namespace StoreProducts.Core.User.RepositoryCommand;

public  interface IUserRegisterRepository : ITransientService
{
    Task<bool> UserRegister(UserRegisterCommand  command);
}