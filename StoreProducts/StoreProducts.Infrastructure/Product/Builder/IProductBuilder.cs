namespace StoreProducts.Infrastructure.Product.Builder;

public interface IProductBuilder
{
    IProductBuilder WithName(string name);
    IProductBuilder WithManufacturePhone(string manufacturePhone);
    IProductBuilder WithManufactureEmail(string manufactureEmail);
    IProductBuilder WithAvailable(bool available);
    IProductBuilder WithProduceDate(DateTime produceDate);
    Core.Product.Entity.Product Build();
}