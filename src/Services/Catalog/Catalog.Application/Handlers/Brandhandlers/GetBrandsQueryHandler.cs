using Catalog.Application.Queries.BrandQueries;
using Catalog.Application.Responses.BrandResponses;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Brandhandlers
{
    public class GetBrandsQueryHandler(IBrandRepository brandRepository) : IRequestHandler<GetBrandsQuery, GetBrandsResult>
    {
        public async Task<GetBrandsResult> Handle(GetBrandsQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Brand> brandList = await brandRepository.GetAllBrandsAsync();

            GetBrandsResult result = new GetBrandsResult(brandList);

            return result;
        }
    }
}
