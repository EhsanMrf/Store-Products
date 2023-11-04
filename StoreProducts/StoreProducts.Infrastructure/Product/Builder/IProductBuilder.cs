using Common.TransientService;

namespace StoreProducts.Infrastructure.Product.Builder;

public interface IProductBuilder : ITransientServiceInfrastructure
{
    IProductBuilder WithName(string name);
    IProductBuilder WithManufacturePhone(string manufacturePhone);
    IProductBuilder WithManufactureEmail(string manufactureEmail);
    IProductBuilder WithAvailable(bool available);
    IProductBuilder WithProduceDate(DateTime produceDate);
    IProductBuilder WithCreateUserById(int createByUserId);
    Core.Product.Entity.Product Build();
}