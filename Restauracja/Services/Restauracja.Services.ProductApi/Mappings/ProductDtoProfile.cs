using AutoMapper;
using Restauracja.Services.ProductApi.Model;
using Restauracja.Services.ProductApi.Model.Dto;

namespace Restauracja.Services.ProductApi.Mappings
{
    public class ProductDtoProfile : Profile
    {
        public ProductDtoProfile()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap();
        }
    }
}
