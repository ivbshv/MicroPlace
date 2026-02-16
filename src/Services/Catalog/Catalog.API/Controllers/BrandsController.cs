using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Catalog.Application.Responses.BrandResponses;
using Catalog.Application.Queries.BrandQueries;

namespace Catalog.API.Controllers
{
    public class BrandsController(IMediator mediator) : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<GetBrandsResult>> GetBrand()
        {
            return await Mediator.Send(new GetBrandsQuery());
        }
    }
}
