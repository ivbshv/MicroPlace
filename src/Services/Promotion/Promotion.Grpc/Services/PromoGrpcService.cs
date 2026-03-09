namespace Promotion.Grpc.Services
{
    public class PromoGrpcService : promoService.promoServiceBase
    {
        public override Task<PromoModel> GetPromo(GetPromoRequest request, ServerCallContext context)
        {
            return base.GetPromo(request, context);
        }
    }
}
