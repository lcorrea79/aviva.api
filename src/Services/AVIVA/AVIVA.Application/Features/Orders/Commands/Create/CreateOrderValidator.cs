using FluentValidation;

namespace AVIVA.Application.Features.Orders.Commands.Create
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.orderRequestDto.Products.Count)
                .GreaterThan(0)
                .WithMessage("The number of products must be greater than 0.");
            RuleFor(x => x.orderRequestDto.Method)
                .NotEmpty()
                .WithMessage("Method is required.");
        }
    }
}
