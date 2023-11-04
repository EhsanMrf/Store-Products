using AutoMapper;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.Query.Result;

namespace StoreProducts.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Core.Product.Entity.Product, UpdateProductCommand>().ReverseMap();
            CreateMap<Core.Product.Entity.Product, ProductResult>().ReverseMap();
        }
    }
}
