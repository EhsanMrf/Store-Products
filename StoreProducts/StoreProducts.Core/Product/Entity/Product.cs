using Common.Entity;

namespace StoreProducts.Core.Product.Entity;

public class Product :BaseEntity<int>
{
    public string Name { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime ProduceDate { get; set; }
}