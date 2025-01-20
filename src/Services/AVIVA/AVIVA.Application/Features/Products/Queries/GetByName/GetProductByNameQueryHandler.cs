using AutoMapper;
using AVIVA.Application.DTOs.Products;
using AVIVA.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Application.Features.Products.Queries.GetByName
{
    public sealed class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByNameQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProductByNameAsync(request.Name);

            return _mapper.Map<List<ProductDto>>(productList);
        }
    }
}