using AVIVA.Application.DTOs.Products;
using MediatR;
using System.Collections.Generic;

namespace AVIVA.Application.Features.Products.Queries.GetAll
{
    public record GetAllProductQuery() : IRequest<List<ProductDto>>;
}