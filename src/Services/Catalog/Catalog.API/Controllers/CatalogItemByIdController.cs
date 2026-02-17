using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{

    public class CatalogItemByIdController(IMediator mediator) : ApiController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCatalogItemByIdResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCatalogItemByIdResult>> GetCatalogItemById(Guid id)
        {
            var result = await Mediator.Send(new GetCatalogItemByIdQuery(id));
            return Ok(result);
        }
    }
}
