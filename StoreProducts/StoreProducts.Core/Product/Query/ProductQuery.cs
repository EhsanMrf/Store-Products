using Common.Input;
using Common.Response;
using MediatR;
using StoreProducts.Core.Product.Query.Result;

namespace StoreProducts.Core.Product.Query;

public class ProductQuery : BaseListInput, IRequest<ServiceResponse<DataList<ProductResult>>>
{
}