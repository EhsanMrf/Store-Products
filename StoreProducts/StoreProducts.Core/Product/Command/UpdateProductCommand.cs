using Common.Response;
using MediatR;

namespace StoreProducts.Core.Product.Command;

public class UpdateProductCommand : Entity.Product, IRequest<ServiceResponse<bool>>
{
}