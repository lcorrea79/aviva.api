using AVIVA.Application.Wrappers;

namespace AVIVA.Application.Mappings;

public class PaginationProfile : GeneralProfile
{
    public PaginationProfile()
    {
        // Configuración genérica para Pagination<T>
        CreateMap(typeof(Pagination<>), typeof(Pagination<>))
            .ForMember("Result", opt => opt.MapFrom(typeof(ItemsResolver<,>)));
    }
}