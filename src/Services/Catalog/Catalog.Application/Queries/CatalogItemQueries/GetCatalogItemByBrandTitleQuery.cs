using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.CatalogItemQueries
{
    public record GetCatalogItemByBrandTitleQuery(string BrandTitle) : IRequest<GetCatalogItemByBrandTitleResult>
    {
    }
}
