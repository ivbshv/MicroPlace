using Catalog.Application.Commands.CatalogItemCommands;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandlers
{
    public class DeleteCatalogItemByIdHandler(ICatalogItemRepository catalogItemRepository)
        : IRequestHandler<DeleteCatalogItemByIdCommand, DeleteCatalogItemByIdResult>
    {
        public async Task<DeleteCatalogItemByIdResult> Handle(DeleteCatalogItemByIdCommand command, CancellationToken cancellationToken)
        {
            var existingItem = await catalogItemRepository.GetCatalogItemAsync(command.Id);

            if (existingItem is null)
            {
                return new DeleteCatalogItemByIdResult(false);
            }

            bool isSuccess = await catalogItemRepository.DeleteCatalogItemAsync(command.Id);
            return new DeleteCatalogItemByIdResult(isSuccess);
        }
    }
}
