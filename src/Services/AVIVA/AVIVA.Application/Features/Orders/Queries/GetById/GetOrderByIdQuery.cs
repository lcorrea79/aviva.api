using AVIVA.Application.Models;
using MediatR;

namespace AVIVa.Application.Features.Orders.Queries.GetById
{
    public record GetOrderByIdQuery(string id) : IRequest<OrderApiResponse>;
}