using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Contexts
{
    public class CatalogContext :DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) :
           base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Catalog>()              
                .HasMany(c => c.ProductList)                
                .WithOne(c => c.Catalog);
        }
        */
    }
}
