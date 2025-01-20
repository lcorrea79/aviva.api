using AutoMapper;
using AVIVa.Application.Features.Orders.Queries.GetById;
using AVIVA.Application.Exceptions;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Application.Models;
using AVIVA.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Application.Features.Orders.Queries.GetById
{
    public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderApiResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IPaymentProviderService _paymentProviderService;
        private readonly PaymentProviderSelector _paymentProviderSelector;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper, IPaymentProviderService paymentProviderService,
            PaymentProviderSelector paymentProviderSelector)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _paymentProviderService = paymentProviderService;
            _paymentProviderSelector = paymentProviderSelector;
        }

        public async Task<OrderApiResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            // Obtener la orden de forma asíncrona
            Order order = await _orderRepository.GetOrderAsync(request.id);

            // Verificar si la orden existe
            if (order == null)
            {
                // Podrías lanzar una excepción o retornar un valor que indique que la orden no fue encontrada
                throw new NotFoundException("id", request.id);
            }

            PaymentProviderConfig provider = _paymentProviderSelector.GetProviderConfigByName(order.ControlData["APIPAGO"].ToString());

            if (provider == null)
            {
                throw new ApiException($"Not found API PAGO: {order.ControlData["APIPAGO"].ToString()}");
            }

            ApiResponse<OrderProvider> paymentResponse = await _paymentProviderService.GetOrder(order.OrderId, provider);


            if (paymentResponse.IsSuccess)
            {
                OrderApiResponse _order = _mapper.Map<OrderApiResponse>(paymentResponse.Data);
                _order.ControlData = order.ControlData;
                return _order;

            }
            else throw new ApiException("Unexpected error");


        }
    }
}