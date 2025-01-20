using AutoMapper;
using AVIVA.Application.Exceptions;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Application.Models;
using AVIVA.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Application.Features.Orders.Commands.Create
{
    public sealed class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, Unit>
    {
        /// <summary>
        /// Unit of work to commit changes to the database
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Repository that handle the logic to save a new client
        /// </summary>
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private IPaymentProviderService _paymentProviderService;
        private IPaymentProviderSelector _paymentProviderSelector;

        public PayOrderCommandHandler(IUnitOfWork unitOfWork,
            IOrderRepository orderRepository,
            ILogger<CreateOrderCommandHandler> logger,
            IMapper mapper,
            IPaymentProviderService paymentProviderService,
            PaymentProviderSelector paymentProviderSelector)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _logger = logger;
            _paymentProviderService = paymentProviderService;
            _paymentProviderSelector = paymentProviderSelector;
        }

        public async Task<Unit> Handle(PayOrderCommand request, CancellationToken cancellationToken)
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

            ApiResponse<string> paymentResponse = await _paymentProviderService.PayOrder(order.OrderId, provider);

            if (paymentResponse.IsSuccess)
            {
                order.Status = Domain.Enums.OrderStatus.Paid;
                // Actualizar la orden en el repositorio
                _orderRepository.Update(order);
                // Guardar los cambios de forma asíncrona
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }


            return Unit.Value;
        }
    }
}