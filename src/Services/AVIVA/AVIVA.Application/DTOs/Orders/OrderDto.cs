using AVIVA.Domain.Enums;
using System.Collections.Generic;

namespace AVIVA.Application.DTOs.Orders
{
    public record OrderDto(
    string OrderId,
    decimal Amount,
    OrderStatus Status,
    PaymentMode? Method,
    Dictionary<string, object>? OtherData,
    Dictionary<string, object>? ControlData
    );


}