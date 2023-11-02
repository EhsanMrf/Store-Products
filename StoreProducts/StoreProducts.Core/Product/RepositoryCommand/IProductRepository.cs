using Common.Response;
using Common.TransientService;
using StoreProducts.Core.Product.Command;

namespace StoreProducts.Core.Product.RepositoryCommand;

public interface IProductRepository : ITransientService
{
    Task<ServiceResponse<Entity.Product>> Create(CreateProductCommand  command);
    Task<ServiceResponse<bool>> Update(UpdateProductCommand command);
    Task<ServiceResponse<bool>> Delete(DeleteProductCommand command);
}