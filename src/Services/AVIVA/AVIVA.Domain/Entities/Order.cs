using AVIVA.Domain.Enums;
using System.Collections.Generic;

namespace AVIVA.Domain.Entities
{
    public class Order
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMode Method { get; set; }
        // Otros datos relevantes y de control
        public Dictionary<string, object> OtherData { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> ControlData { get; set; } = new Dictionary<string, object>();

        // Constructor predeterminado
        public Order() { }

    }
}
