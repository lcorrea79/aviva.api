using AVIVA.Application.DTOs.Orders;
using MediatR;
using System.Collections.Generic;

namespace AVIVA.Application.Features.Orders.Queries.GetAll
{
    public record GetAllOrderQuery() : IRequest<List<OrderDto>>;
}