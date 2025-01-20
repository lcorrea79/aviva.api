using AutoMapper;
using AVIVA.Application.DTOs.Products;
using AVIVA.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Application.Features.Products.Queries.GetAll
{
    public sealed class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProductAsync();

            return _mapper.Map<List<ProductDto>>(productList);
        }
    }
}