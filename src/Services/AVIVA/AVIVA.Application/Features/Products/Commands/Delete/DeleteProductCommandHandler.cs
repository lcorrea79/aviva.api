using AVIVA.Application.Exceptions;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id) ?? throw new ValidationException("Service Code not found");
            _productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}