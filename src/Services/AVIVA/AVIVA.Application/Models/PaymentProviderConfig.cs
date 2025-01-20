using AVIVA.Domain.Enums;
using System.Collections.Generic;

namespace AVIVA.Application.Models
{
    public class PaymentProviderConfig
    {
        public string Name { get; set; }
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string EndpointCancel { get; set; }
        public string EndpointPay { get; set; }
        public Dictionary<PaymentMode, string> Method { get; set; }

        public List<CommissionRule> CommissionRules { get; set; } = new List<CommissionRule>();
    }
}
