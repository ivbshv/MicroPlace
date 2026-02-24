using Carter;
using Mapster;
using MediatR;

namespace Basket.API.ShoppingBasket.Retrieve
{
    public class RetrieveCartEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/cart/{accountName}", async (string accountName, ISender sender) =>
            {
                var result = await sender.Send(new RetrieveCartQuery(accountName));
                var response = result.Adapt<RetrieveCartResponse>();
                return Results.Ok(response);
            })
            .WithName("RetrieveCartEndpoint")
             .Produces<RetrieveCartResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Получение корзины")
             .WithDescription("Возвращает корзину пользователя по имени аккаунта");
        }
    }
}
