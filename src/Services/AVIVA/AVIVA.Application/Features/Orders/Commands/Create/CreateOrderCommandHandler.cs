using AutoMapper;
using AVIVA.Application.Exceptions;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Order = AVIVA.Domain.Entities.Order;

namespace AVIVA.Application.Features.Orders.Commands.Create
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderApiResponse>
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
        private readonly IMapper _mapper;
        private readonly IPaymentProviderService _paymentProviderService;
        private readonly IPaymentProviderSelector _paymentProviderSelector;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork,
            IOrderRepository orderRepository,
            ILogger<CreateOrderCommandHandler> logger,
            IMapper mapper,
            IPaymentProviderService paymentProviderService,
            PaymentProviderSelector paymentProviderSelector
            )
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
            _paymentProviderService = paymentProviderService;
            _paymentProviderSelector = paymentProviderSelector;
        }

        public async Task<OrderApiResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // validate incoming request
            var validator = new CreateOrderValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError($"Create Order failed, Validator error(s): {string.Join(", ", validationResult.Errors)}");
                throw new ValidationException("Incorrect data", validationResult);
            }

            var amount = request.orderRequestDto.Products.Sum(product => product.UnitPrice);
            // Seleccionar el proveedor de pago óptimo
            var optimalProvider = _paymentProviderSelector.GetOptimalProvider(request.orderRequestDto.Method, amount);

            ApiResponse<OrderProvider> paymentResponse = await _paymentProviderService.AddOrder(request.orderRequestDto, optimalProvider);

            Order newOrder = new Order();

            // Si el pago fue exitoso, actualizar el estado de la orden, si es necesario
            if (paymentResponse.IsSuccess)
            {
                newOrder.OrderId = paymentResponse.Data.OrderId;
                newOrder.ControlData.Add("createdDate", paymentResponse.Data.CreatedDate);
                newOrder.ControlData.Add("APIPAGO", optimalProvider.Name);
                newOrder.Status = Domain.Enums.OrderStatus.Pending;
                newOrder.Method = request.orderRequestDto.Method;
                newOrder.Amount = paymentResponse.Data.Amount;
            }
            else
            {
                // Manejar el fallo en el pago, dependiendo de los detalles de la respuesta
                _logger.LogError($"Payment failed for Order ID:. Reason: {paymentResponse.Message}");
                //throw new Exceptions($"Payment failed for Order ID: {order.OrderId}. Reason: {paymentResponse.Status}");
            }

            await _orderRepository.AddAsync(newOrder);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            OrderApiResponse _order = _mapper.Map<OrderApiResponse>(paymentResponse.Data);
            _order.ControlData.Add("APIPAGO", optimalProvider.Name);

            return _order;
        }

    }
}