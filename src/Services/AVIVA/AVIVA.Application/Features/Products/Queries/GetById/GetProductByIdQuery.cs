using AVIVA.Application.DTOs.Products;
using MediatR;

namespace AVIVa.Application.Features.Products.Queries.GetById
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
}