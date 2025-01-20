using AVIVA.Application.Exceptions;
using AVIVA.Application.Features.Orders.Commands.Cancel;
using AVIVA.Application.Features.Orders.Commands.Create;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Application.Models;
using AVIVA.Domain.Entities;
using AVIVA.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;

namespace AVIVA.Application.Test.Features.Orders.Commands.Cancel
{
    public class CancelOrderCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<ILogger<CreateOrderCommandHandler>> _logger;
        private readonly Mock<IPaymentProviderService> _paymentProviderService;
        private readonly PaymentProviderSelector _paymentProviderSelector;

        public CancelOrderCommandHandlerTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _orderRepository = new Mock<IOrderRepository>();
            _logger = new Mock<ILogger<CreateOrderCommandHandler>>();
            _paymentProviderService = new Mock<IPaymentProviderService>();


            var mockOptions = new Mock<IOptions<List<PaymentProviderConfig>>>();

            mockOptions.Setup(o => o.Value).Returns(new List<PaymentProviderConfig>
        {
            new PaymentProviderConfig
            {
                Name = "MockProvider",
                Method = new Dictionary<PaymentMode, string>
                {
                    { PaymentMode.Card, "Card" }
                },
                CommissionRules = new List<CommissionRule>
                {
                    new CommissionRule
                    {
                        PaymentMode = PaymentMode.Card,
                        MinAmount = 100,
                        MaxAmount = 1000,
                        FixedFee = 5,
                        PercentageFee = 2
                    }
                }
            }
        });

            // Asignar la instancia de PaymentProviderSelector directamente a la propiedad
            _paymentProviderSelector = new PaymentProviderSelector(mockOptions.Object);

            // Configurar unit of work para simular guardado exitoso
            _unitOfWork.Setup(u => u.SaveChangesAsync(CancellationToken.None))
                .ReturnsAsync(1);
        }

        [Fact]
        public async Task Handle_ShouldUpdateOrderStatusToPaid_WhenPaymentIsSuccessful()
        {
            // Arrange
            var orderId = "12345";
            var order = new Order
            {
                OrderId = orderId,
                Status = OrderStatus.Pending,
                ControlData = new Dictionary<string, object> { { "APIPAGO", "MockProvider" } }
            };

            var providerConfig = new PaymentProviderConfig { Name = "MockProvider" };

            _orderRepository.Setup(repo => repo.GetOrderAsync(orderId))
                .ReturnsAsync(order);


            _paymentProviderService.Setup(s => s.CancelOrder(It.IsAny<string>(), It.IsAny<PaymentProviderConfig>()))
           .ReturnsAsync(new ApiResponse<string>(true, "Cancellation Successful", "Ok"));

            var handler = new CancelOrderCommandHandler(
                _unitOfWork.Object,
                _orderRepository.Object,
                _logger.Object,
                null,
                _paymentProviderService.Object,
                _paymentProviderSelector);

            var command = new CancelOrderCommand(orderId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBe(Unit.Value);
            order.Status.ShouldBe(OrderStatus.Cancelled);

            _orderRepository.Verify(repo => repo.Update(order), Times.Once);
            _unitOfWork.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = "nonexistentOrder";
            _orderRepository.Setup(repo => repo.GetOrderAsync(orderId))
                .ReturnsAsync((Order)null);

            var handler = new CancelOrderCommandHandler(
                _unitOfWork.Object,
                _orderRepository.Object,
                _logger.Object,
                null,
                _paymentProviderService.Object,
                _paymentProviderSelector);

            var command = new CancelOrderCommand(orderId);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowApiException_WhenProviderNotFound()
        {
            // Arrange
            var orderId = "12345";
            var order = new Order
            {
                OrderId = orderId,
                Status = OrderStatus.Pending,
                ControlData = new Dictionary<string, object> { { "APIPAGO", "UnknownProvider" } }
            };

            _orderRepository.Setup(repo => repo.GetOrderAsync(orderId))
                .ReturnsAsync(order);


            var handler = new CancelOrderCommandHandler(
                _unitOfWork.Object,
                _orderRepository.Object,
                _logger.Object,
                null,
                _paymentProviderService.Object,
                _paymentProviderSelector);

            var command = new CancelOrderCommand(orderId);

            // Act & Assert
            await Should.ThrowAsync<ApiException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
