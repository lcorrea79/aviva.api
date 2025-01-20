using FluentValidation;

namespace AVIVA.Application.Features.Orders.Commands.Create
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            //  RuleFor(x => x.orderDto.Amount).NotEmpty().WithMessage("Amount is required");
            //RuleFor(x => x.orderDto.Details).NotEmpty().WithMessage("Details is required");
            //RuleFor(x => x.orderDto.Price).NotEmpty().WithMessage("Price is required");
        }
    }
}
