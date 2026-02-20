using Catalog.Application.Commands.CatalogItemCommands;
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCatalogItemByIdResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCatalogItemByIdResult>> GetCatalogItemById(Guid id)
        {
            var result = await Mediator.Send(new GetCatalogItemByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("title/{catalogItemTitle}")]
        [ProducesResponseType(typeof(GetCatalogItemByTitleResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCatalogItemByTitleResult>> GetByTitle(string catalogItemTitle)
        {
            var result = await Mediator.Send(new GetCatalogItemByTitleQuery(catalogItemTitle));
            return Ok(result);
        }

        [HttpGet("title/{brandTitle}")]
        [ProducesResponseType(typeof(GetCatalogItemByBrandTitleResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCatalogItemByBrandTitleResult>> GetByBrandTitle(string brandTitle)
        {
            var result = await Mediator.Send(new GetCatalogItemByBrandTitleQuery(brandTitle));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateCatalogItemResult), StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateCatalogItemResult>> CreateCatalogItem(
            [FromBody] CreateCatalogItemCommand command
        )
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(
                nameof(GetCatalogItemById),
                new { id = result.Id },
                result
            );
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateCatalogItemResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateCatalogItemResult>> UpdateCatalogItem(
            [FromBody] UpdateCatalogItemCommand command
        )
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteCatalogItemByIdResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DeleteCatalogItemByIdResult>> DeleteCatalogItem(Guid id)
        {
            var result = await Mediator.Send(new DeleteCatalogItemByIdCommand(id));
            return Ok(result);
        }
    }
}
