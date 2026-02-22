using Catalog.Application.Queries.CategoryQueries;
using Catalog.Application.Responses.CategoryResponses;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Categoryhandlers
{
    public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, GetCategoriesResult>
    {
        public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Category> categories = await categoryRepository.GetAllCategoriesAsync();
            GetCategoriesResult result = new GetCategoriesResult(categories);
            return result;
        }
    }
}
