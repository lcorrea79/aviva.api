using AVIVA.Application.DTOs.Orders;
using AVIVA.Application.Models;
using AVIVA.Domain.Entities;
using System.Collections.Generic;

namespace AVIVA.Application.Mappings;

public class OrderProfile : GeneralProfile
{
    public OrderProfile()
    {

        // Mapeo de Order a OrderDto y viceversa
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Method)) // Mapeo explícito de Method (opcional)
            .ForMember(dest => dest.OtherData, opt => opt.MapFrom(src => src.OtherData)) // Copia directa de OtherData
            .ForMember(dest => dest.ControlData, opt => opt.MapFrom(src => src.ControlData)) // Copia directa de ControlData
            .ReverseMap();

        CreateMap<OrderProvider, OrderApiResponse>()
             .ForMember(dest => dest.ControlData, opt => opt.MapFrom(src => new Dictionary<string, object>
             {
                { "CreatedDate", src.CreatedDate }
             }))
            .ForMember(dest => dest.Fees, opt => opt.MapFrom(src => src.Fees))  // Mapear Fees
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products)); // Mapear Products
    }

}