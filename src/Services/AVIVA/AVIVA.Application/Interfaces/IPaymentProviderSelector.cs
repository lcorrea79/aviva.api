using AVIVA.Application.Models;
using AVIVA.Domain.Enums;

namespace AVIVA.Application.Interfaces
{
    public interface IPaymentProviderSelector
    {
        PaymentProviderConfig GetOptimalProvider(PaymentMode mode, decimal amount);
        PaymentProviderConfig GetProviderConfigByName(string name);
    }
}