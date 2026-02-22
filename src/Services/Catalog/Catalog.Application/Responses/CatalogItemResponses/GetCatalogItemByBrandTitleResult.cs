using Catalog.Domain.Entities;

namespace Catalog.Application.Responses.CatalogItemResponses
{
    public record GetCatalogItemByBrandTitleResult(IEnumerable<CatalogItem> Result)
    {
    }
}
