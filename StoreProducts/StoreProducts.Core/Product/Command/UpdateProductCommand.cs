using Common.Response;
using MediatR;

namespace StoreProducts.Core.Product.Command;

public class UpdateProductCommand : IRequest<ServiceResponse<bool>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
}