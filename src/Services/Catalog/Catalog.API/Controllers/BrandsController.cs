using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Catalog.Application.Responses.BrandResponses;
using Catalog.Application.Queries.BrandQueries;
using Microsoft.CodeAnalysis.Operations;
using System.Net;

namespace Catalog.API.Controllers
{
    public class BrandsController(IMediator mediator) : ApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetBrandsResult),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBrandsResult>> GetBrand()
        {
            var result = await Mediator.Send(new GetBrandsQuery());
            return Ok(result);
        }
    }
}
