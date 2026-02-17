using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class GetCatalogItemByBrandTitleController(IMediator mediator) : ApiController
    {
        [HttpGet("title/{brandTitle}")]
        [ProducesResponseType(typeof(GetCatalogItemByBrandTitleResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCatalogItemByBrandTitleResult>> GetByBrandTitle(string brandTitle)
        {
            var result = await Mediator.Send(new GetCatalogItemByBrandTitleQuery(brandTitle));
            return Ok(result);
        }
    }
}
