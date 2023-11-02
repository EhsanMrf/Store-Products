using Common.TransientService;
using StoreProducts.Core.User.Command;

namespace StoreProducts.Core.User.RepositoryCommand;

public  interface IRegisterRepository : ITransientService
{
    Task<bool> UserRegister(UserRegisterCommand  command);
}