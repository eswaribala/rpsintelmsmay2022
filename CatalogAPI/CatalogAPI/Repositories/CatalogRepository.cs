using CatalogAPI.Contexts;
using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogContext _dbContext;

        public CatalogRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Catalog> AddCatalog(Catalog Catalog)
        {
            var result = await this._dbContext.Catalogs.AddAsync(Catalog);

            await this._dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> DeleteCatalog(long CatalogId)
        {
            var result = await this._dbContext.Catalogs.FirstOrDefaultAsync(c =>
           c.CatalogId == CatalogId);
            if (result != null)
            {
                this._dbContext.Catalogs.Remove(result);
                await this._dbContext.SaveChangesAsync();
            }

            result = await GetCatalogById(CatalogId);
            if (result == null)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<Catalog>> GetAllCatalog()
        {
           return await this._dbContext.Catalogs.ToListAsync();

        }

        public async Task<Catalog> GetCatalogById(long CatalogId)
        {
            var result= await this._dbContext.Catalogs.FirstOrDefaultAsync(c => 
            c.CatalogId == CatalogId);
            if (result != null)
                return result;
            return null;
        }

        public async Task<Catalog> UpdateCatalog(Catalog Catalog)
        {
            var result = await _dbContext.Catalogs
                .FirstOrDefaultAsync(c => c.CatalogId ==Catalog.CatalogId);

            if (result != null)
            {
                result.CatalogName = Catalog.CatalogName;



                await _dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

       
    }
}
