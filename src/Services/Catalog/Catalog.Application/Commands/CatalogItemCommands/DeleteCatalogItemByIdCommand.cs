using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Catalog.Application.Commands.CatalogItemCommands
{
    public record DeleteCatalogItemByIdCommand(Guid Id) 
        : IRequest<DeleteCatalogItemByIdResult>;
    
}
