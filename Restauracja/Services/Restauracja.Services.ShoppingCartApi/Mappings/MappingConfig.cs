using AutoMapper;
using Restauracja.Services.ShoppingCartApi.Model.Dto;
using Restauracja.Services.ShoppingCartApi.Model;

namespace Restauracja.Services.ShoppingCartApi.Mappings
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
        }
    }
}
