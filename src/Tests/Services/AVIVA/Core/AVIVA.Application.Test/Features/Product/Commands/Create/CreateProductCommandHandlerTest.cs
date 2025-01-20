using AutoMapper;
using AVIVA.Application.DTOs.Products;
using AVIVA.Application.Features.Products.Commands.Create;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Application.Mappings;
using AVIVA.Application.Test.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace AVIVA.Application.Test.Features.Products.Commands.Create
{
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepository = MockProductRepository.GetProductRepository();
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<CreateProductCommandHandler>> _logger;
        private readonly IMapper _mapper;

        public CreateProductCommandHandlerTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(1));
            _logger = new Mock<ILogger<CreateProductCommandHandler>>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductProfile());
            });
            _mapper = mapperConfig.CreateMapper();

        }

        [Fact]
        public async Task Handle_Create_Product()
        {
            // Arrange
            var handler = new CreateProductCommandHandler(_unitOfWork.Object, _productRepository.Object, _logger.Object, _mapper);


            var productDto = new ProductDto(
                3,
                "Product # 1",
                "Product detail",
                20,
                true);

            // Act
            var command = new CreateProductCommand(productDto);
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var repo = await _productRepository.Object.GetAllAsync();
            repo.Count().ShouldBe(3);
            var product = await _productRepository.Object.GetByIdAsync(3);
            product.Name.ShouldBe("Product # 1");
            product.Details.ShouldBe("Product detail");
            product.UnitPrice.ShouldBe(20);
            product.Status.ShouldBe(true);
        }
    }
}
