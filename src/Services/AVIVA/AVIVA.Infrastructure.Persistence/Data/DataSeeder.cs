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
    new Product("Bluetooth Speaker", "Portable speaker with high-quality sound", 49.99m, true)
                /*new Product("Tablet Pro", "Large display with stylus support", 799.99m, true),
                new Product("Wireless Charger", "Fast charging for compatible devices", 39.99m, true),
                new Product("Drone", "Quadcopter with HD camera and GPS", 499.99m, true),
                new Product("Fitness Tracker", "Water-resistant wearable for activity monitoring", 99.99m, true),
                new Product("External Hard Drive", "1TB capacity with fast data transfer", 59.99m, true),
                new Product("Virtual Reality Headset", "Immersive VR experience for gaming", 399.99m, true),
                new Product("Smart Home Hub", "Control all your smart devices from one place", 129.99m, true),
                new Product("Electric Toothbrush", "Rechargeable with multiple brushing modes", 59.99m, true),
                new Product("Gaming Mouse Pad", "Large surface with RGB lighting", 24.99m, true)*/
                );

                context.SaveChanges();
            }
        }
    }
}
