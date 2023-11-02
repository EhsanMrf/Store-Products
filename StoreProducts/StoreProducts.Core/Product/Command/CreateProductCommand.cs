using Common.Response;
using MediatR;

namespace StoreProducts.Core.Product.Command;

public class CreateProductCommand :IRequest<ServiceResponse<Entity.Product>>
{
    public string Name { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime ProduceDate { get; set; }
}