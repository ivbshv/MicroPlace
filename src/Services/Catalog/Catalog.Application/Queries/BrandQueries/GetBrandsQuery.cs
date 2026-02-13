using Catalog.Application.Responses.BrandResponses;
using Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.BrandQueries
{
    public record GetBrandsQuery : IRequest<GetBrandsResult>
    {
    }
}
