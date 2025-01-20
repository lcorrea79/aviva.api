using System.Collections.Generic;

namespace AVIVA.Application.Models
{
    public class OrderApiResponse : OrderPrimary
    {
        public Dictionary<string, object> OtherData { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> ControlData { get; set; } = new Dictionary<string, object>();

        // Constructor
        public OrderApiResponse() { }

    }
}
