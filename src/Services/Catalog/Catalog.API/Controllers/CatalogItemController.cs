using Catalog.Application.Queries.BrandQueries;
using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.BrandResponses;
using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogItemController(IMediator mediator) : ApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetCatalogItemsResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCatalogItemsResult>> GetCatalogItems()
        {
            var result = await Mediator.Send(new GetCatalogItemsQuery());
            return Ok(result);
        }
    }
}
