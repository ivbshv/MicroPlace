using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commands.CatalogItemCommands
{
    public record CreateCatalogItemCommand(
        string? Title,
        string? ShortDescription,
        string? FullDescription,
        string? ImageUrl,
        Brand? Brand,
        Category? Category,
        decimal Price
    ) : IRequest<CreateCatalogItemResult>
    {
    }
}
