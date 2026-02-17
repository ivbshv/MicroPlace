using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogItemByTitleController : ApiController
    {
        [HttpGet("title/{catalogItemTitle}")]
        [ProducesResponseType(typeof(GetCatalogItemByTitleResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCatalogItemByTitleResult>> GetByTitle(string catalogItemTitle)
        {
            var result = await Mediator.Send(new GetCatalogItemByTitleQuery(catalogItemTitle));
            return Ok(result);
        }
    }
}
