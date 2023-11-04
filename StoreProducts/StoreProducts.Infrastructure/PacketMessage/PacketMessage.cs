namespace StoreProducts.Infrastructure.PacketMessage;

public class PacketMessage : IPackageMessage
{
    public string InvalidCreateProduct() => "You are not allowed to register twice";
    public string InvalidUpdateProduct() => "You are not allowed to edit a duplicate name";
    public string NotCreatorProduct() => "You cannot perform this operation because you are not its creator";
}