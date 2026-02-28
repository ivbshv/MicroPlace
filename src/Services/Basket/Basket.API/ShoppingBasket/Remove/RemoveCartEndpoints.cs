using Basket.API.ShoppingBasket.Save;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.ShoppingBasket.Remove
{
    public class RemoveCartEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/cart/{accountName}", async (string accountName, ISender sender) =>
            {
                var result = await sender.Send(new RemoveCartCommand(accountName));
                var response = result.Adapt<RemoveCartResponse>();
                return Results.Ok(response);
            })
             .WithName("Remove Cart")
             .Produces<RemoveCartResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .ProducesProblem(StatusCodes.Status404NotFound)
             .WithSummary("Удаление корзины по accountName")
             .WithDescription("Удаляет корзину пользователя по accountName");
        }
    }
}
