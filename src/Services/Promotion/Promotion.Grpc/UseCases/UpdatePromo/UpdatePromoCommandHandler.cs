namespace Promotion.Grpc.UseCases.UpdatePromo
{
    public class UpdatePromoCommandHandler(IPromoRepository repository) : ICommandHandler<UpdatePromoCommand, UpdatePromoResponse>
    {
        public async Task<UpdatePromoResponse> Handle(UpdatePromoCommand command, CancellationToken cancellationToken)
        {
            var promo = new Promo
            {
                Id = Guid.Parse(command.Promo.Id),
                Title = command.Promo.Title,
                Value = (decimal)command.Promo.Value
            };

            var success = await repository.UpdateAsync(promo, cancellationToken);

            return new UpdatePromoResponse
            {
                Success = success,
                Description = success ? "Обновлено" : "Промо не найдено"
            };
        }
    }
}
