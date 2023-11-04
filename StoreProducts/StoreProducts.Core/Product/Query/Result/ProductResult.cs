namespace StoreProducts.Core.Product.Query.Result;

public class ProductResult
{
    public string Name { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime ProduceDate { get; set; }
}