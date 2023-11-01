using Common.Response;
using MediatR;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.RepositoryCommand;

namespace StoreProducts.CoreService.Product.CommandHandler;

public class ProductCommandHandler
    :IRequestHandler<CreateProductCommand,ServiceResponse<Core.Product.Entity.Product>>,
    IRequestHandler<UpdateProductCommand,ServiceResponse<bool>>,
    IRequestHandler<DeleteProductCommand,ServiceResponse<bool>>

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

    public async Task<ServiceResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        return await _repository.Update(request);
    }

    public async Task<ServiceResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await _repository.Delete(request);
    }
}