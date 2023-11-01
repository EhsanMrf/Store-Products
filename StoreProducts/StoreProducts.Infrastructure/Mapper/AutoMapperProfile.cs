using AutoMapper;
using StoreProducts.Core.Product.Command;

namespace StoreProducts.Infrastructure.Mapper
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
        CreateMap<Core.Product.Entity.Product,UpdateProductCommand>().ReverseMap();

        }
    }
}
