using MediatR;

namespace AVIVA.Application.Features.Orders.Commands.Cancel
{
    public record CancelOrderCommand(string id) : IRequest<Unit>;
}