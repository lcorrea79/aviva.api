using AVIVA.Application.DTOs.Orders;
using AVIVA.Application.Models;
using System.Threading.Tasks;

namespace AVIVA.Application.Interfaces
{
    public interface IPaymentProviderService
    {
        Task<ApiResponse<OrderProvider>> AddOrder(OrderRequestDto orderDto, PaymentProviderConfig paymentProviderConfig);
        Task<ApiResponse<OrderProvider>> GetOrder(string orderId, PaymentProviderConfig paymentProviderConfig);
        // Task<ApiResponse<List<OrderProvider>>> GetAllOrders(string orderId, PaymentProviderConfig paymentProviderConfig);
        Task<ApiResponse<string>> CancelOrder(string OrderId, PaymentProviderConfig paymentProviderConfig);
        Task<ApiResponse<string>> PayOrder(string OrderId, PaymentProviderConfig paymentProviderConfig);
    }
}
