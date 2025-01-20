using Moq;
using AVIVA.Application.DTOs.Orders;
using AVIVA.Application.Models;
using AVIVA.Application.Interfaces;


namespace AVIVA.Application.Test.Mocks
{
    public class MockPaymentProviderService
    {
        public Mock<IPaymentProviderService> CreateMock()
        {
            // Crear un Mock de HttpClient
            var mockHttpClient = new Mock<HttpClient>(Mock.Of<HttpMessageHandler>());

            // Crear el Mock de PaymentProviderService con el HttpClient
            var mockPaymentProviderService = new Mock<IPaymentProviderService>(mockHttpClient.Object);

            // Configuración para el método AddOrder
            mockPaymentProviderService.Setup(service => service.AddOrder(It.IsAny<OrderRequestDto>(), It.IsAny<PaymentProviderConfig>()))
                .ReturnsAsync(new ApiResponse<OrderProvider>(true, "Success", new OrderProvider
                {
                    OrderId = "12345",
                    CreatedDate = DateTime.Now,
                    Amount = 500
                }));

            // Configuración para el método GetOrder
            mockPaymentProviderService.Setup(service => service.GetOrder(It.IsAny<string>(), It.IsAny<PaymentProviderConfig>()))
                .ReturnsAsync(new ApiResponse<OrderProvider>(true, "Success", new OrderProvider
                {
                    OrderId = "12345",
                    CreatedDate = DateTime.Now,
                    Amount = 500
                }));

            // Configuración para el método CancelOrder
            mockPaymentProviderService.Setup(service => service.CancelOrder(It.IsAny<string>(), It.IsAny<PaymentProviderConfig>()))
                .ReturnsAsync(new ApiResponse<string>(true, "Success", "OK"));

            // Configuración para el método PayOrder
            mockPaymentProviderService.Setup(service => service.PayOrder(It.IsAny<string>(), It.IsAny<PaymentProviderConfig>()))
                .ReturnsAsync(new ApiResponse<string>(true, "Success", "OK"));

            return mockPaymentProviderService;
        }
    }
}