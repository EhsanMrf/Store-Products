using Common.Response;
using Common.TransientService;
using StoreProducts.Core.Product.Command;

namespace StoreProducts.Core.Product.RepositoryCommand;

public interface IProductRepository : ITransientService
{
    Task<ServiceResponse<Entity.Product>> Create(ProductCommand  command);
}