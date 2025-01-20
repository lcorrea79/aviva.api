using AVIVA.Application.Models;
using AVIVA.Domain.Enums;
using Moq;
using Microsoft.Extensions.Options;

namespace AVIVA.Application.Test.Mocks
{
    public class MockPaymentProviderSelector
    {
        public static Mock<PaymentProviderSelector> GetMockPaymentProviderSelector()
        {
            // Crear un mock de IOptions<List<PaymentProviderConfig>> para inyectar en el PaymentProviderSelector
            var mockOptions = new Mock<IOptions<List<PaymentProviderConfig>>>();

            // Simular una lista de PaymentProviderConfig
            var mockProviders = new List<PaymentProviderConfig>
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
            },
            new PaymentProviderConfig
            {
                Name = "MockProvider2",
                Method = new Dictionary<PaymentMode, string>
                {
                    { PaymentMode.Card, "Card" }
                },
                CommissionRules = new List<CommissionRule>
                {
                    new CommissionRule
                    {
                        PaymentMode = PaymentMode.Card,
                        MinAmount = 200,
                        MaxAmount = 2000,
                        FixedFee = 10,
                        PercentageFee = 1
                    }
                }
            }
        };

            mockOptions.Setup(o => o.Value).Returns(mockProviders);

            // Crear el mock de PaymentProviderSelector usando el mock de IOptions
            var mockPaymentProviderSelector = new Mock<PaymentProviderSelector>(mockOptions.Object);

            // Configurar el método GetOptimalProvider para que devuelva un proveedor basado en el PaymentMode y el monto
            mockPaymentProviderSelector.Setup(s => s.GetOptimalProvider(It.IsAny<PaymentMode>(), It.IsAny<decimal>()))
                .Returns<PaymentMode, decimal>((mode, amount) =>
                {
                    return mockProviders
                        .Where(p => p.CommissionRules.Any(r => r.PaymentMode == mode &&
                                                               (r.MinAmount == null || amount >= r.MinAmount) &&
                                                               (r.MaxAmount == null || amount <= r.MaxAmount)))
                        .OrderBy(p => p.CommissionRules
                            .FirstOrDefault(r => r.PaymentMode == mode &&
                                                 (r.MinAmount == null || amount >= r.MinAmount) &&
                                                 (r.MaxAmount == null || amount <= r.MaxAmount))
                            .FixedFee)
                        .FirstOrDefault();
                });

            // Configurar el método GetProviderConfigByName para devolver un proveedor específico por nombre
            mockPaymentProviderSelector.Setup(s => s.GetProviderConfigByName(It.IsAny<string>()))
                .Returns<string>((name) => mockProviders.FirstOrDefault(p => p.Name == name));

            return mockPaymentProviderSelector;
        }
    }
}

