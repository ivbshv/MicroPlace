namespace Promotion.Grpc.UseCases.UpdatePromo
{
    public class UpdatePromoCommandValidator : AbstractValidator<UpdatePromoCommand>
    {
        public UpdatePromoCommandValidator()
        {
            RuleFor(x => x.Promo)
                .NotNull()
                .WithMessage("Данные промо-акции обязательны.");

            When(x => x.Promo is not null, () =>
            {
                RuleFor(x => x.Promo.Id)
                    .NotEmpty()
                    .WithMessage("Идентификатор промо-акции обязателен.")
                    .Must(id => Guid.TryParse(id, out _))
                    .WithMessage("Идентификатор промо-акции должен быть корректным GUID.");

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