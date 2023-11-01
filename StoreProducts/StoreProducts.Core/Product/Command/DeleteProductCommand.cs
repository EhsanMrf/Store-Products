using Common.Response;
using MediatR;

namespace StoreProducts.Core.Product.Command;

public class DeleteProductCommand : IRequest<ServiceResponse<bool>>
{
    public int Id { get; set; }
}