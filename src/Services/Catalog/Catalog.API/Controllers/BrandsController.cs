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
            var result =  Mediator.Send(new GetBrandsQuery());
            return Ok(result);
        }
    }
}
