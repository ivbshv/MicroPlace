using MediatR;
using Promotion.Grpc.UseCases.GetPromo;

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
    }
}
