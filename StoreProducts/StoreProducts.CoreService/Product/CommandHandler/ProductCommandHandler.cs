using Common.BaseService;
using Common.Response;
using MediatR;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.RepositoryCommand;

namespace StoreProducts.CoreService.Product.CommandHandler;

public class ProductCommandHandler:IRequestHandler<CreateProductCommand,ServiceResponse<Core.Product.Entity.Product>>
{
    private readonly IProductRepository _repository;

    public ProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<Core.Product.Entity.Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        return await _repository.Create(request);
    }
}