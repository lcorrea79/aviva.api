using AVIVA.Application.DTOs.Products;
using AVIVA.Domain.Enums;
using System.Collections.Generic;

namespace AVIVA.Application.DTOs.Orders
{
    public record OrderRequestDto(
     List<ProductProviderDto> Products,
     PaymentMode Method
    );


}