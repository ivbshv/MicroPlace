namespace Promotion.Grpc.UseCases.GetPromo
{
    public record GetPromoByCatalogItemIdQuery(string CatalogItemId) : IQuery<PromoModel>;
   
}
