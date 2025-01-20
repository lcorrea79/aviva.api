namespace AVIVA.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Details { get; private set; }
        public decimal UnitPrice { get; private set; }
        public bool Status { get; private set; }


        private Product()
        {
        }

        public Product(string name, string details, decimal price, bool status)
        {
            Name = name;
            Details = details;
            UnitPrice = price;
            Status = status;
        }

        public void Update(int id, string name, string details, decimal price, bool status)
        {
            Id = id;
            Name = name;
            Details = details;
            UnitPrice = price;
            Status = status;

        }
    }
}