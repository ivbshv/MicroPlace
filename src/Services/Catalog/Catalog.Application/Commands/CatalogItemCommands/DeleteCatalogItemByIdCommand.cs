using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commands.CatalogItemCommands
{
    public record DeleteCatalogItemByIdCommand(Guid Id) 
        : IRequest<DeleteCatalogItemByIdResult>;
    
}
