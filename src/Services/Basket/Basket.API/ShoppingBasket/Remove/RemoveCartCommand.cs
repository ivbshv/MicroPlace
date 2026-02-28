using Basket.API.Models;
using Common.Kernel.CQRS.Commands;
using System.Windows.Input;

namespace Basket.API.ShoppingBasket.Remove
{
    public record RemoveCartCommand(string AccountName) : ICommand<RemoveCartResult>;


}
