using MediatR;

namespace AVIVA.Application.Features.Orders.Commands.Create
{
    public record PayOrderCommand(string id) : IRequest<Unit>;
}