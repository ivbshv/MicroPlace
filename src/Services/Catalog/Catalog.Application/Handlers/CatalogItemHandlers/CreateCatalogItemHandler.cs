using Catalog.Application.Commands.CatalogItemCommands;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.CatalogItemHandlers
{
    internal class CreateCatalogItemHandler(ICatalogItemRepository catalogItemRepository)
        : IRequestHandler<CreateCatalogItemCommand, CreateCatalogItemResult>
    {
        public async Task<CreateCatalogItemResult> Handle(CreateCatalogItemCommand command, CancellationToken cancellationToken)
        {
            var catalogItem = command.Adapt<CatalogItem>();
            catalogItem.Id = Guid.NewGuid();
            await catalogItemRepository.CreateCatalogItemAsync(catalogItem);
            return new CreateCatalogItemResult(catalogItem.Id);
        }
    }
}
