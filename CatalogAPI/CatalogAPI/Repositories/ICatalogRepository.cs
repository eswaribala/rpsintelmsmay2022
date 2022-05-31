using CatalogAPI.Models;

namespace CatalogAPI.Repositories
{
    public interface ICatalogRepository
    {
       Task<Catalog> AddCatalog(Catalog Catalog);
        Task<Catalog> UpdateCatalog(Catalog Catalog);
        Task DeleteCatalog(Catalog Catalog);
        Task<Catalog> GetCatalogById(long CatalogId);
        Task<IEnumerable<Catalog>> GetAllCatalog();
        




    }
}
