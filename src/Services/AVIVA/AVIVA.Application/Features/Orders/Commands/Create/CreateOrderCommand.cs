using AVIVA.Application.DTOs.Orders;
using AVIVA.Application.Models;
using MediatR;

namespace AVIVA.Application.Features.Orders.Commands.Create
{
    public record CreateOrderCommand(OrderRequestDto orderRequestDto) : IRequest<OrderApiResponse>;
}