using AutoMapper;
using AVIVA.Application.Features.Products.Commands.Delete;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Application.Mappings;
using AVIVA.Application.Test.Mocks;
using Moq;
using Shouldly;

namespace AVIVA.Application.Test.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepository = MockProductRepository.GetProductRepository();
        private readonly Mock<IUnitOfWork> _unitOfWork;
        public DeleteProductCommandHandlerTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(1));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductProfile());
            });
        }

        [Fact]
        public async Task Handle_Delete_Product()
        {
            // Arrange
            var handler = new DeleteProductCommandHandler(_productRepository.Object, _unitOfWork.Object);

            var productId = 1;

            // Act
            var command = new DeleteProductCommand(productId);
            await handler.Handle(command, CancellationToken.None);

            // Assert

            var products = await _productRepository.Object.GetAllAsync();
            products.Count().ShouldBe(1);

        }
    }
}
