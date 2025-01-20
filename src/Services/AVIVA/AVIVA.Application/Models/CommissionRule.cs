using AVIVA.Domain.Enums;

namespace AVIVA.Application.Models
{
    public class CommissionRule
    {
        public PaymentMode PaymentMode { get; set; }
        public decimal? FixedFee { get; set; }
        public decimal? PercentageFee { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
    }
}
