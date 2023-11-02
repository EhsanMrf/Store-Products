using Common.TransientService;

namespace StoreProducts.Infrastructure.PacketMessage;

public interface IPackageMessage :ITransientServiceInfrastructure
{
    string InvalidCreateProduct();
    public string InvalidUpdateProduct();
}