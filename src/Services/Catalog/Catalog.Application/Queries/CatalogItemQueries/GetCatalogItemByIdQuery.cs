using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.CatalogItemQueries
{
    public record GetCatalogItemByIdQuery(Guid Id) :IRequest<GetCatalogItemByIdResult>
    {
    }
}
