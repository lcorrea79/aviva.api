using AVIVA.Application.DTOs.Products;
using AVIVA.Domain.Entities;

namespace AVIVA.Application.Mappings;

public class ProductProfile : GeneralProfile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();

        CreateMap<ProductDto, ProductProviderDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
    }
}