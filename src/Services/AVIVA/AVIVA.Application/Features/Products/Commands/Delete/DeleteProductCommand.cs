using MediatR;
namespace AVIVA.Application.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(int Id) : IRequest<bool>;
}
