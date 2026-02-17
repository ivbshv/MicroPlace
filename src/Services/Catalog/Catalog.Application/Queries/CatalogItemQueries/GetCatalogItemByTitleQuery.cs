using Catalog.Application.Handlers.CatalogItemHandlers;
using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.CatalogItemQueries
{
    public record GetCatalogItemByTitleQuery(string Title) : IRequest<GetCatalogItemByTitleResult>
    {
    }
}
