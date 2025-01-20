using FluentValidation;

namespace AVIVA.Application.Features.Products.Commands.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.productDto.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.productDto.Details).NotEmpty().WithMessage("Details is required");
            RuleFor(x => x.productDto.UnitPrice).NotEmpty().WithMessage("Price is required");
        }
    }
}
