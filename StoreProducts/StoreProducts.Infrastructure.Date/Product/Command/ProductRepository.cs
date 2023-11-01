using Common.OperationCrud;
using Common.Response;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.RepositoryCommand;
using StoreProducts.Infrastructure.Database;
using StoreProducts.Infrastructure.Product.Builder;

namespace StoreProducts.Infrastructure.Date.Product.Command;

public class ProductRepository : IProductRepository
{
    private readonly ICrudManager<Core.Product.Entity.Product,int,DatabaseContext> _repositoryManager;
    private readonly IProductBuilder _productBuilder;
    public ProductRepository(ICrudManager<Core.Product.Entity.Product, int, DatabaseContext> repositoryManager, IProductBuilder productBuilder)
    {
        _repositoryManager = repositoryManager;
        _productBuilder = productBuilder;
    }

    public async Task<ServiceResponse<Core.Product.Entity.Product>> Create(ProductCommand command)
    {
        var product = _productBuilder.WithName(command.Name)
            .WithAvailable(command.IsAvailable)
            .WithManufactureEmail(command.ManufactureEmail)
            .WithManufacturePhone(command.ManufacturePhone)
            .WithProduceDate(command.ProduceDate).Build();

       return await _repositoryManager.Insert<Core.Product.Entity.Product>(product);
    }
}