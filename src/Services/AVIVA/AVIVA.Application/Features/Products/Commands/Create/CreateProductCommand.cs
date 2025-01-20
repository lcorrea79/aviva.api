using AVIVA.Application.DTOs.Products;
using MediatR;

namespace AVIVA.Application.Features.Products.Commands.Create
{
    public record CreateProductCommand(ProductDto productDto) : IRequest<Unit>;
}