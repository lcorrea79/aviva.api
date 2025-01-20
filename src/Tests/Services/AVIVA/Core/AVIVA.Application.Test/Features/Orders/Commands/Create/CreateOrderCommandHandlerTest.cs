using AutoMapper;
using AVIVA.Application.DTOs.Orders;
using AVIVA.Application.DTOs.Products;
using AVIVA.Application.Features.Orders.Commands.Create;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Models;
using AVIVA.Application.Test.Mocks;
using AVIVA.Domain.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using AVIVA.Application.Mappings;
using Shouldly;
using AVIVA.Domain.Entities;

public class CreateOrderCommandHandlerTest
{
    private readonly Mock<IOrderRepository> _orderRepository = MockOrderRepository.GetOrderRepository();
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<ILogger<CreateOrderCommandHandler>> _logger;
    private readonly IMapper _mapper;
    private readonly Mock<IPaymentProviderService> _paymentProviderService;
    private readonly PaymentProviderSelector _paymentProviderSelector;

    public CreateOrderCommandHandlerTest()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(u => u.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(1));
        _logger = new Mock<ILogger<CreateOrderCommandHandler>>();
        _paymentProviderService = new Mock<IPaymentProviderService>();

        var mockOptions = new Mock<IOptions<List<PaymentProviderConfig>>>();
        mockOptions.Setup(o => o.Value).Returns(new List<PaymentProviderConfig>
        {
            new PaymentProviderConfig
            {
                Name = "MockProvider1",
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

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new OrderProfile());
        });
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Handle_Create_Order()
    {
        // Arrange
        var handler = new CreateOrderCommandHandler(
            _unitOfWork.Object,
            _orderRepository.Object,
            _logger.Object,
            _mapper,
            _paymentProviderService.Object,
            _paymentProviderSelector
        );

        var orderDto = new OrderRequestDto(
            Method: PaymentMode.Card,
            Products: new List<ProductProviderDto>
            {
                new ProductProviderDto("Laptop Dell", 300),
                new ProductProviderDto("Laptop Lenovo", 200)
            }
        );

        // Simular que `AddOrder` devuelve una respuesta válida
        _paymentProviderService.Setup(s => s.AddOrder(It.IsAny<OrderRequestDto>(), It.IsAny<PaymentProviderConfig>()))
            .ReturnsAsync(new ApiResponse<OrderProvider>(true, "Success", new OrderProvider
            {
                OrderId = "12345",
                CreatedDate = DateTime.Now,
                Amount = 500
            }));

        // Act
        var command = new CreateOrderCommand(orderDto);
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.ShouldNotBeNull();
        response.ControlData.ShouldContainKey("APIPAGO"); // Verifica que el provider fue agregado
        response.ControlData["APIPAGO"].ShouldBe("MockProvider1");

        // Verificar que el repositorio ha agregado una nueva orden
        _orderRepository.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Once);
        _unitOfWork.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
