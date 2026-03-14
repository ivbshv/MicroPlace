namespace Promotion.Grpc.UseCases.UpdatePromo
{
    public record UpdatePromoCommand(UpdatePromoRequest Promo) : ICommand<UpdatePromoResponse>;
}
