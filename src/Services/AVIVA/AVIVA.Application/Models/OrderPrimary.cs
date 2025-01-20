using AVIVA.Application.DTOs.Orders;
using AVIVA.Application.DTOs.Products;
using System.Collections.Generic;

namespace AVIVA.Application.Models
{
    public class OrderPrimary
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Method { get; set; }
        public List<FeeDto> Fees { get; set; }
        public List<ProductProviderDto> Products { get; set; }

        // Constructor
        public OrderPrimary() { }

    }
}
