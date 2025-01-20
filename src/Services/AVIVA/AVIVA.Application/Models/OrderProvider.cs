using System;

namespace AVIVA.Application.Models
{
    public class OrderProvider : OrderPrimary
    {

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

    }
}
