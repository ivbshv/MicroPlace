namespace Promotion.Grpc.Services
{
    public class PromoGrpcService(IMediator mediator) : promoService.promoServiceBase
    {
        public override async Task<PromoModel> GetPromo(GetPromoRequest request, ServerCallContext context)
        {
            var query = new GetPromoByCatalogItemIdQuery(request.CatalogItemId);
            var result = await mediator.Send(query);
            return result;
        }

        public override async Task<CreatePromoResponse> CreatePromo(CreatePromoRequest request, ServerCallContext context)
        {
            var command = new CreatePromoCommand(request);
            var result = await mediator.Send(command);
            return result;
        }
    }
}
