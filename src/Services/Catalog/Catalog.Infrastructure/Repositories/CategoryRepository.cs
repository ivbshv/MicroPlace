using Catalog.Domain.Repositories;
using Marten;

namespace Catalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDocumentSession _documentSession;

        public CategoryRepository(IDocumentSession session)
        {
            _documentSession = session;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _documentSession.Query<Category>().ToListAsync(cancellationToken);
        }
    }
}
