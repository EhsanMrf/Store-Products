using System.Linq.Expressions;
using Common.BaseService;
using Common.Jwt;
using Common.OperationCrud;
using Common.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.RepositoryCommand;
using StoreProducts.Infrastructure.CurrentIdentity;
using StoreProducts.Infrastructure.Database;
using StoreProducts.Infrastructure.PacketMessage;
using StoreProducts.Infrastructure.Product.Builder;

namespace StoreProducts.Infrastructure.Date.Product.Command;

public class ProductRepository : IdentityUserCurrent, IProductRepository
{
    private readonly ICrudManager<Core.Product.Entity.Product, int, DatabaseContext> _repositoryManager;
    private readonly IProductBuilder _productBuilder;
    private readonly IPackageMessage _packageMessage;

    public ProductRepository(IHttpContextAccessor accessor, UserManager<Core.User.Entity.User> userManager, IPackageMessage packageMessage, ICrudManager<Core.Product.Entity.Product, int, DatabaseContext> repositoryManager, IProductBuilder productBuilder) : base(accessor, userManager)
    {
        _packageMessage = packageMessage;
        _repositoryManager = repositoryManager;
        _productBuilder = productBuilder;
    }

    public override Expression<Func<Core.User.Entity.User, Core.User.Entity.User>> GetUserBySelect()
    {
        return x => new Core.User.Entity.User { Id = x.Id };
    }
    public async Task<ServiceResponse<Core.Product.Entity.Product>> Create(CreateProductCommand command)
    {
        if (await HasRecordByName(command.Name))
            Invalid(false, message: _packageMessage.InvalidCreateProduct());

        var currentUser = await CurrentUser();
        return await _repositoryManager.Insert<Core.Product.Entity.Product>(InstanceProduct(command, currentUser.Id));
    }

    public async Task<ServiceResponse<bool>> Update(UpdateProductCommand command)
    {
        var currentUser = await CurrentUser();
        if (!await HasRecordCreateUserById(currentUser.Id))
            return Invalid(false, message: _packageMessage.NotCreatorProduct());
        
        if (await HasRecordBeforeUpdateByName(command))
            return Invalid(false, message: _packageMessage.InvalidUpdateProduct());

        return await _repositoryManager.UpdateById(command.Id, command);
    }

    public async Task<ServiceResponse<bool>> Delete(DeleteProductCommand command)
    {
        var currentUser = await CurrentUser();
        if (!await HasRecordCreateUserById(currentUser.Id))
            return Invalid(false, message: _packageMessage.NotCreatorProduct());

        return await _repositoryManager.DeleteById(command.Id);
    }

    private async Task<bool> HasRecordByName(string name) => await _repositoryManager.HasRecord(q => q.Name.Equals(name));
    private async Task<bool> HasRecordCreateUserById(int userId) => await _repositoryManager.HasRecord(q => q.CreateByUserId== userId);
    private async Task<bool> HasRecordBeforeUpdateByName(UpdateProductCommand command)
    {
        var hasRecord = await _repositoryManager.SelectByPredicate(q => q.Name.Equals(command.Name), i => i.Id);
        return hasRecord > 0 && hasRecord != command.Id;
    }

    private Core.Product.Entity.Product InstanceProduct(CreateProductCommand command, int userId)
    {
        return _productBuilder.WithName(command.Name)
            .WithAvailable(command.IsAvailable)
            .WithManufactureEmail(command.ManufactureEmail)
            .WithManufacturePhone(command.ManufacturePhone)
            .WithCreateUserById(userId)
            .WithProduceDate(command.ProduceDate).Build();
    }
}