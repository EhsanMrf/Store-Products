using System.Net.Http.Headers;

namespace StoreProducts.Infrastructure.Product.Builder;

public class ProductBuilder : IProductBuilder
{
    private string Name { get; set; }
    private string ManufacturePhone { get; set; }
    private string ManufactureEmail { get; set; }
    private bool IsAvailable { get; set; }
    private DateTime ProduceDate { get; set; }
    public int CreateByUserId { get; set; }

    public IProductBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public IProductBuilder WithManufacturePhone(string manufacturePhone)
    {
        ManufacturePhone = manufacturePhone;
        return this;
    }

    public IProductBuilder WithManufactureEmail(string manufactureEmail)
    {
        ManufactureEmail = manufactureEmail;
        return this;
    }

    public IProductBuilder WithAvailable(bool available)
    {
        IsAvailable=available;
        return this;
    }

    public IProductBuilder WithProduceDate(DateTime produceDate)
    {
        ProduceDate = produceDate;
        return this;
    }

    public Core.Product.Entity.Product Build()
    {
        return new Core.Product.Entity.Product
        {
            IsAvailable = IsAvailable,
            Name = Name,
            CreateDateTime = DateTime.Now,
            ManufactureEmail = ManufactureEmail,
            ManufacturePhone = ManufacturePhone,
            ProduceDate = ProduceDate,
        };
    }
}