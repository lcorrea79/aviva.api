namespace AVIVA.Domain.Entities
{
    public class Fee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string OrderId { get; set; }  // Clave foránea a Order
    }
}
