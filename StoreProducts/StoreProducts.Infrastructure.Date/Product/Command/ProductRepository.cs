using Common.BaseService;
using Common.OperationCrud;
using Common.Response;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.RepositoryCommand;
using StoreProducts.Infrastructure.Database;
using StoreProducts.Infrastructure.PacketMessage;
using StoreProducts.Infrastructure.Product.Builder;

namespace StoreProducts.Infrastructure.Date.Product.Command;

public class ProductRepository : BaseService,IProductRepository
{
    private readonly ICrudManager<Core.Product.Entity.Product,int,DatabaseContext> _repositoryManager;
    private readonly IProductBuilder _productBuilder;
    private readonly IPackageMessage _packageMessage;
    public ProductRepository(ICrudManager<Core.Product.Entity.Product, int, DatabaseContext> repositoryManager, IProductBuilder productBuilder, IPackageMessage packageMessage)
    {
        _repositoryManager = repositoryManager;
        _productBuilder = productBuilder;
        _packageMessage = packageMessage;
    }

    public async Task<ServiceResponse<Core.Product.Entity.Product>> Create(CreateProductCommand command)
    {
        if (await HasRecordByName(command.Name))
            Invalid(false, message: _packageMessage.InvalidCreateProduct());

        var serviceResponse = await _repositoryManager.Insert<Core.Product.Entity.Product>(InstanceProduct(command));
        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> Update(UpdateProductCommand command)
    {
        if (await HasRecordBeforeUpdateByName(command))
            return Invalid(false, message: _packageMessage.InvalidUpdateProduct());

        return await _repositoryManager.UpdateById(command.Id, command);
    }

    public async Task<ServiceResponse<bool>> Delete(DeleteProductCommand command)
    {
        return await _repositoryManager.DeleteById(command.Id);
    }

    private async Task<bool> HasRecordByName(string name)=> await _repositoryManager.HasRecord(q => q.Name.Equals(name));

    private async Task<bool> HasRecordBeforeUpdateByName(UpdateProductCommand command)
    {
        var hasRecord = await _repositoryManager.SelectByPredicate(q=>q.Name.Equals(command.Name),i=>i.Id);
        return hasRecord > 0 && hasRecord != command.Id;
    }

    private Core.Product.Entity.Product InstanceProduct(CreateProductCommand command)
    {
        return _productBuilder.WithName(command.Name)
            .WithAvailable(command.IsAvailable)
            .WithManufactureEmail(command.ManufactureEmail)
            .WithManufacturePhone(command.ManufacturePhone)
            .WithProduceDate(command.ProduceDate).Build();
    }
}