using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Models.Sqlite
{
    public class CustomerSQLiteDatabaseContext : DbContext
    {
        public CustomerSQLiteDatabaseContext(DbContextOptions<CustomerSQLiteDatabaseContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .HasMany(x => x.InvoiceList);
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
