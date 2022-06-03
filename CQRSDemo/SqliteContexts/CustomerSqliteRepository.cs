using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Models.Sqlite
{
    public class CustomerSQLiteRepository
    {
        private readonly CustomerSQLiteDatabaseContext _context;
        public CustomerSQLiteRepository(CustomerSQLiteDatabaseContext context)
        {
            _context = context;
        }
        public Customer Create(Customer customer)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Customer> entry = 
                _context.Customers.Add(customer);
            _context.SaveChanges();
            return entry.Entity;
        }
        public void Update(Customer customer)
        {
            _context.SaveChanges();
        }
        public void Remove(long id)
        {
            _context.Customers.Remove(GetById(id));
            _context.SaveChanges();
     }
        public IQueryable<Customer> GetAll()
        {
            return _context.Customers;
        }
        public Customer GetById(long id)
        {
            return _context.Customers.Find(id);
        }
    }
}
