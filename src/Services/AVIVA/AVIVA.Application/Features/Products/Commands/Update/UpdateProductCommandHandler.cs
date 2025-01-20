using AVIVA.Application.Exceptions;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        /// <summary>
        /// Unit of work to commit changes to the database
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository that handle the logic to save a new client
        /// </summary>
        private readonly IProductRepository _productRepository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork,
                       IProductRepository productRepository,
                       ILogger<UpdateProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // validate incoming request
            var validator = new UpdateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError($"Update Product failed, Validator error(s): {string.Join(", ", validationResult.Errors)}");
                throw new ValidationException("Incorrect data", validationResult);
            }

            var product = await _productRepository.GetByIdAsync(request.productDto.Id);

            product.Update(request.productDto.Id, request.productDto.Name, request.productDto.Details, request.productDto.UnitPrice, request.productDto.Status);
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}