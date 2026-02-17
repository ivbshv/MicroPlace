using Catalog.Application.Responses.CategoryResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.CategoryQueries
{
    public record GetCategoriesQuery : IRequest<GetCategoriesResult>
    {
    }
}
