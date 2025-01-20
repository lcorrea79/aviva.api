using AVIVA.Application.DTOs.Products;
using MediatR;

namespace AVIVA.Application.Features.Products.Commands.Update
{
    public record UpdateProductCommand(ProductDto productDto) : IRequest<Unit>;
}