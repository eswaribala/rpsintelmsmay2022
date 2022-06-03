using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Models.Mongo
{
    public class CustomerMongoRepository
    {
        private const string _customerDB = "IntelCustomerDB";
        private const string _customerCollection = "IntelCustomers";
        private IMongoDatabase _db;
        public CustomerMongoRepository()
        {
            MongoClient _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase(_customerDB);
        }
        public List<CustomerEntity> GetCustomers()
        {
            return _db.GetCollection<CustomerEntity>(_customerCollection).Find(_ => true).ToList();
        }
        public CustomerEntity GetCustomer(long id)
        {
            return _db.GetCollection<CustomerEntity>(_customerCollection).Find
                (customer => customer.CustomerId == id).SingleOrDefault();
        }
        public CustomerEntity GetCustomerByEmail(string email)
        {
            return _db.GetCollection<CustomerEntity>(_customerCollection).Find
                (customer => customer.Email == email).Single();
        }
        public void Create(CustomerEntity customer)
        {
            _db.GetCollection<CustomerEntity>(_customerCollection).InsertOne(customer);
        }
        public void Update(CustomerEntity customer)
        {
            var filter = Builders<CustomerEntity>.Filter.Where(_ => _.CustomerId == customer.CustomerId);
            _db.GetCollection<CustomerEntity>(_customerCollection).ReplaceOne(filter, customer);
        }
        public void Remove(long id)
        {
            var filter = Builders<CustomerEntity>.Filter.Where(_ => _.CustomerId == id);
            var operation = _db.GetCollection<CustomerEntity>(_customerCollection).
                DeleteOne(filter);
        }
    }
}
