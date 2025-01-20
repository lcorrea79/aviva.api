using AutoMapper;
using AVIVA.Application.DTOs.Orders;
using AVIVA.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Application.Features.Orders.Queries.GetAll
{
    public sealed class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetAllOrderAsync();

            return _mapper.Map<List<OrderDto>>(orderList);
        }
    }
}