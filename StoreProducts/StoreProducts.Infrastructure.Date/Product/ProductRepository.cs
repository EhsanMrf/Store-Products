using System.Linq.Expressions;
using System.Security.Principal;
using AutoMapper;
using Common.BaseService;
using Common.Jwt;
using Common.OperationCrud;
using Common.Response;
using Common.Response.PageListExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.Query;
using StoreProducts.Core.Product.Query.Result;
using StoreProducts.Core.Product.RepositoryCommand;
using StoreProducts.Infrastructure.CurrentIdentity;
using StoreProducts.Infrastructure.Database;
using StoreProducts.Infrastructure.PacketMessage;
using StoreProducts.Infrastructure.Product.Builder;

namespace StoreProducts.Infrastructure.Date.Product;

public class ProductRepository : IdentityUserCurrent, IProductRepository
{
    private readonly ICrudManager<Core.Product.Entity.Product, int, DatabaseContext> _repositoryManager;
    private readonly IProductBuilder _productBuilder;
    private readonly IPackageMessage _packageMessage;
    private readonly IMapper _mapper;

    public ProductRepository(IHttpContextAccessor accessor, UserManager<Core.User.Entity.User> userManager, IPackageMessage packageMessage, ICrudManager<Core.Product.Entity.Product, int, DatabaseContext> repositoryManager, IProductBuilder productBuilder, IMapper mapper) : base(accessor, userManager)
    {
        _packageMessage = packageMessage;
        _repositoryManager = repositoryManager;
        _productBuilder = productBuilder;
        _mapper = mapper;
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

    public async Task<ServiceResponse<DataList<ProductResult>>> GetAll(ProductQuery query)
    {
        var products = await _repositoryManager.GetEntity()
            .ToPagedListAsync(query.Page,query.PageSize,_mapper, new ProductResult());
        return Success(products);
    }

    private async Task<bool> HasRecordByName(string name) => await _repositoryManager.HasRecord(q => q.Name.Equals(name));
    private async Task<bool> HasRecordCreateUserById(int userId) => await _repositoryManager.HasRecord(q => q.CreateByUserId == userId);
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