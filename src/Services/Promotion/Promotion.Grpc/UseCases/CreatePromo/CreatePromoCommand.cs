namespace Promotion.Grpc.UseCases.CreatePromo
{
    public record CreatePromoCommand(CreatePromoRequest Promo) : ICommand<CreatePromoResponse>;
}
