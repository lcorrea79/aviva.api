using AVIVA.Application.DTOs.Products;
using MediatR;
using System.Collections.Generic;

namespace AVIVA.Application.Features.Products.Queries.GetByName
{
    public record GetProductByNameQuery(string Name) : IRequest<List<ProductDto>>;
}