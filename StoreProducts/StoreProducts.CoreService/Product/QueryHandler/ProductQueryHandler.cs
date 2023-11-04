using Common.Response;
using MediatR;
using StoreProducts.Core.Product.Query;
using StoreProducts.Core.Product.Query.Result;
using StoreProducts.Core.Product.RepositoryCommand;

namespace StoreProducts.CoreService.Product.QueryHandler
{
    public class ProductQueryHandler : IRequestHandler<ProductQuery,ServiceResponse<DataList<ProductResult>>>
    {
        private readonly IProductRepository _repository;

        public ProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<DataList<ProductResult>>> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}