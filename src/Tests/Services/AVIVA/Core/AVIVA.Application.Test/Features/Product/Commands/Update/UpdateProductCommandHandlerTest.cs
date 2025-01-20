using AVIVA.Application.DTOs.Products;
using AVIVA.Application.Features.Products.Commands.Update;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Application.Test.Mocks;
using AVIVA.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace AVIVA.Application.Test.Features.Products.Commands.Update;

public class UpdateProductCommandHandlerTest
{
    private readonly Mock<IProductRepository> _productRepository = MockProductRepository.GetProductRepository();
    private readonly Mock<IUnitOfWork> _unitOfWork = MockProductRepository.GetUnitOfWork();
    private readonly Mock<ILogger<UpdateProductCommandHandler>> _logger;

    public UpdateProductCommandHandlerTest()
    {
        _unitOfWork.Setup(u => u.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(1));
        _logger = new Mock<ILogger<UpdateProductCommandHandler>>();
    }

    [Fact]
    public async Task Handle_Update_Product()
    {
        // Arrange
        var handler = new UpdateProductCommandHandler(_unitOfWork.Object, _productRepository.Object, _logger.Object);

        var productDto = new ProductDto
        (
             2,
             "Product Test 2 Update",
             "1234567890",
             200,
           true);

        // Act
        var command = new UpdateProductCommand(productDto);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<Unit>();
        var products = await _productRepository.Object.GetAllAsync();
        var enumerable = products as Product[] ?? products.ToArray();
        enumerable.Count().ShouldBe(2);
        enumerable.First(c => c.Id == 2).Name.ShouldBe("Product Test 2 Update");
    }
}