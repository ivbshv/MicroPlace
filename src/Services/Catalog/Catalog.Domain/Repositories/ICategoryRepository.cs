namespace Catalog.Domain.Repositories
{
    internal interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
