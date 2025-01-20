using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Domain.Entities;
using Moq;

namespace AVIVA.Application.Test.Mocks
{
    internal static class MockProductRepository
    {
        public static Mock<IProductRepository> GetProductRepository()
        {
            var productRepository = new Mock<IProductRepository>();


            var productList = new List<Product>
            {
                new Product( "Product # 1","Product 1 Details",10, true),
                new Product("Product Test 2","Product 2 Details",200, false)
        };

            productList[0].Id = 1;
            productList[1].Id = 2;

            productRepository.Setup(p => p.GetAllAsync().Result).Returns(productList);
            productRepository.Setup(p => p.GetByIdAsync(It.IsAny<int>()).Result).Returns((int i) => productList.Single(p => p.Id == i));
            productRepository.Setup(p => p.AddAsync(It.IsAny<Product>())).Callback((Product product) => productList.Add(product));
            productRepository.Setup(p => p.Update(It.IsAny<Product>())).Callback((Product product) =>
                {
                    var index = productList.FindIndex(p => p.Id == product.Id);
                    productList[index] = product;
                });
            productRepository.Setup(p => p.Delete(It.IsAny<Product>())).Callback((Product product) => productList.Remove(product));


            return productRepository;
        }

        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            // Mock IUnitOfWork
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            return unitOfWorkMock;
        }
    }
}
