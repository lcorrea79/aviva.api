using AVIVA.Application.Interfaces;
using AVIVA.Domain.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AVIVA.Application.Models
{
    public class PaymentProviderSelector : IPaymentProviderSelector
    {
        private readonly IEnumerable<PaymentProviderConfig> _providers;

        public PaymentProviderSelector(IOptions<List<PaymentProviderConfig>> options)
        {
            _providers = options.Value;
        }

        public PaymentProviderConfig GetOptimalProvider(PaymentMode mode, decimal amount)
        {
            return _providers
                .Where(p => p.CommissionRules.Any(r => r.PaymentMode == mode &&
                                                       (r.MinAmount == null || amount >= r.MinAmount) &&
                                                       (r.MaxAmount == null || amount <= r.MaxAmount)))
                .OrderBy(p => CalculateFee(p, mode, amount))
                .FirstOrDefault();
        }

        public PaymentProviderConfig GetProviderConfigByName(string name)
        {
            return _providers.SingleOrDefault(p => p.Name == name);
        }

        private decimal CalculateFee(PaymentProviderConfig provider, PaymentMode mode, decimal amount)
        {
            var rule = provider.CommissionRules
                .FirstOrDefault(r => r.PaymentMode == mode &&
                                     (r.MinAmount == null || amount >= r.MinAmount) &&
                                     (r.MaxAmount == null || amount <= r.MaxAmount));

            if (rule == null)
            {
                throw new InvalidOperationException($"No commission rule found for {provider.Name} with mode {mode} and amount {amount}.");
            }

            decimal fee = 0;
            if (rule.FixedFee.HasValue)
            {
                fee += rule.FixedFee.Value;
            }
            if (rule.PercentageFee.HasValue)
            {
                fee += amount * (rule.PercentageFee.Value / 100);
            }

            return fee;
        }
    }
}
