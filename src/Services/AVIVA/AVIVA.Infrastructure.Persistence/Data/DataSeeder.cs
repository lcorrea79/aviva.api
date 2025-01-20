using AVIVA.Domain.Entities;
using AVIVA.Infrastructure.Persistence.Contexts;
using System.Linq;
namespace AVIVA.Infrastructure.Persistence.Data
{
    public static class DataSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Verifica si ya existen datos en la tabla
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product("Laptop Pro", "High-end laptop for professionals", 5299.99m, true),
                new Product("Wireless Mouse", "Ergonomic design with Bluetooth connectivity", 29.99m, true),
                new Product("Mechanical Keyboard", "RGB backlighting and durable switches", 89.99m, true),
                new Product("Smartphone X", "Latest model with advanced features", 999.99m, true),
                new Product("Noise-Cancelling Headphones", "Over-ear headphones with active noise cancellation", 199.99m, true),
                new Product("4K Monitor", "Ultra HD resolution with vibrant colors", 399.99m, true),
                new Product("Gaming Chair", "Comfortable chair for long gaming sessions", 149.99m, true),
                new Product("Portable SSD", "Fast external storage with USB-C support", 79.99m, true),
                new Product("Action Camera", "Compact camera with 4K recording", 299.99m, true),
                new Product("Smartwatch", "Feature-packed wearable with fitness tracking", 199.99m, true),
                new Product("Bluetooth Speaker", "Portable speaker with high-quality sound", 49.99m, true));

                context.SaveChanges();
            }
        }
    }
}
