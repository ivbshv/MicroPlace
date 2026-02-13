namespace Catalog.Domain.Repositories
{
    internal interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
    }
}
