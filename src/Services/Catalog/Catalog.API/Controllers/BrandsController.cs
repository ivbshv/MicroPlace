using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Catalog.Application.Responses.BrandResponses;
using Catalog.Application.Queries.BrandQueries;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetBrandsResult>> GetBrand()
        {
            return await mediator.Send(new GetBrandsQuery());
        }
    }
}
