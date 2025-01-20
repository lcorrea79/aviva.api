using FluentValidation;

namespace AVIVA.Application.Features.Products.Commands.Update
{
    internal sealed class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.productDto.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.productDto.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.productDto.Details).NotEmpty().WithMessage("Details is required");
            RuleFor(x => x.productDto.UnitPrice).NotEmpty().WithMessage("UnitPrice is required");
        }
    }

}
