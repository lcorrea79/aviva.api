using AutoMapper;
using AVIVA.Application.Exceptions;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Application.Features.Products.Commands.Create
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        /// <summary>
        /// Unit of work to commit changes to the database
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository that handle the logic to save a new client
        /// </summary>
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            ILogger<CreateProductCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // validate incoming request
            var validator = new CreateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError($"Create Product failed, Validator error(s): {string.Join(", ", validationResult.Errors)}");
                throw new ValidationException("Incorrect data", validationResult);
            }

            var product = _mapper.Map<Product>(request.productDto);

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}