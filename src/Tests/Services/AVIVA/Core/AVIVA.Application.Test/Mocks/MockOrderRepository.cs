using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Domain.Entities;
using Moq;

namespace AVIVA.Application.Test.Mocks
{
    internal static class MockOrderRepository
    {
        public static Mock<IOrderRepository> GetOrderRepository()
        {
            var orderRepository = new Mock<IOrderRepository>();


            // Crear productos
            var product1 = new Product("Laptop Dell", "High-end laptop", 200, true);
            var product2 = new Product("Laptop Lenovo", "Affordable laptop", 100, true);
            var product3 = new Product("Smartphone Samsung", "Latest model", 500, true);

            // Crear listas de productos para las órdenes
            var productsOrder1 = new List<Product> { product1 };
            var productsOrder2 = new List<Product> { product1, product2 };
            var productsOrder3 = new List<Product> { product2, product3 };

            // Crear las tres órdenes con diferentes estados y métodos de pago
            var order1 = new Order();

            var order2 = new Order();

            var order3 = new Order();


            /*// Agregar comisiones a las órdenes
            order1.AddFee(new Fee { Name = "Purchase Fee", Amount = 1.45m });
            order1.AddFee(new Fee { Name = "Interchange Fee", Amount = 0.60m });
            order1.OrderId = "1";

            order2.AddFee(new Fee { Name = "Purchase Fee", Amount = 1.45m });
            order2.AddFee(new Fee { Name = "Interchange Fee", Amount = 0.60m });
            order2.OrderId = "2";

            order3.AddFee(new Fee { Name = "Purchase Fee", Amount = 1.45m });
            order3.AddFee(new Fee { Name = "Interchange Fee", Amount = 0.60m });
            order3.OrderId = "3";

            // Cambiar estado de la primera orden a 'Paid'
            order1.Pay();

            // Cambiar estado de la segunda orden a 'Cancelled' con motivo
            order2.Cancel("Customer request");*/

            var orderList = new List<Order>();

            orderList.Add(order1);
            orderList.Add(order2);
            orderList.Add(order3);


            orderRepository.Setup(p => p.GetAllAsync().Result).Returns(orderList);
            orderRepository.Setup(p => p.GetByIdAsync(It.IsAny<int>()).Result).Returns((string i) => orderList.Single(p => p.OrderId == i));
            orderRepository.Setup(p => p.AddAsync(It.IsAny<Order>())).Callback((Order order) => orderList.Add(order));
            orderRepository.Setup(p => p.Update(It.IsAny<Order>())).Callback((Order order) =>
                {
                    var index = orderList.FindIndex(p => p.OrderId == order.OrderId);
                    orderList[index] = order;
                });
            orderRepository.Setup(p => p.Delete(It.IsAny<Order>())).Callback((Order order) => orderList.Remove(order));


            return orderRepository;
        }

        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            // Mock IUnitOfWork
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            return unitOfWorkMock;
        }
    }
}
