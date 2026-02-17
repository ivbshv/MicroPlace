using Catalog.Application.Queries.BrandQueries;
using Catalog.Application.Queries.CategoryQueries;
using Catalog.Application.Responses.CategoryResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CategoriesController(IMediator mediator) : ApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetCategoriesResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCategoriesResult>> GetCategories()
        {
            var result = await Mediator.Send(new GetCategoriesQuery());

            return Ok(result);
        }
    }
}
