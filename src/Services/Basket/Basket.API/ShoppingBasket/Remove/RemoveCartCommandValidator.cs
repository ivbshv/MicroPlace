using FluentValidation;

namespace Basket.API.ShoppingBasket.Remove
{
    public class RemoveCartCommandValidator : AbstractValidator<RemoveCartCommand>
    {
        public RemoveCartCommandValidator()
        {
            RuleFor(x => x.AccountName).NotEmpty().WithMessage("AccountName не может быть пустым");
        }
    }
}
