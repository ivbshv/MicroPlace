using Common.Kernel.CQRS.Commands;

namespace Promotion.Grpc.UseCases.CreatePromo
{
    public record CreatePromoCommand(CreatePromoRequest Promo) : ICommand<CreatePromoResponse>;
}
