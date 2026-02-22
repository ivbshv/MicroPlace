using Catalog.Domain.Repositories;
using Marten;

namespace Catalog.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IDocumentSession _documentSession;

        public BrandRepository(IDocumentSession session)
        {
            _documentSession = session;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken)
        {
            return await _documentSession.Query<Brand>().ToListAsync(cancellationToken);
        }
    }
}
