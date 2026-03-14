namespace Promotion.Grpc.UseCases.CreatePromo
{
    public class CreatePromoCommandValidator : AbstractValidator<CreatePromoCommand>
    {
        public CreatePromoCommandValidator()
        {
            RuleFor(x => x.Promo)
                .NotNull()
                .WithMessage("Данные промо-акции обязательны.");

            When(x => x.Promo is not null, () =>
            {
                RuleFor(x => x.Promo.CatalogItemId)
                    .NotEmpty()
                    .WithMessage("CatalogItemId обязателен.")
                    .MaximumLength(100)
                    .WithMessage("CatalogItemId не должен превышать 100 символов.");

                RuleFor(x => x.Promo.Title)
                    .NotEmpty()
                    .WithMessage("Название обязательно.")
                    .MaximumLength(200)
                    .WithMessage("Название не должно превышать 200 символов.");

                RuleFor(x => x.Promo.Value)
                    .Must(v => !double.IsNaN(v) && !double.IsInfinity(v))
                    .WithMessage("Значение должно быть корректным числом.")
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Значение должно быть больше или равно 0.");
            });
        }
    }
}