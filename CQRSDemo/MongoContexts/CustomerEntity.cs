using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Models.Mongo
{
    public class CustomerEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("CustomerId")]
        public long CustomerId { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("CustomerName")]
        public string CustomerName { get; set; }
        [BsonElement("MobileNo")]
        public long MobileNo { get; set; }
       
        [BsonElement("InvoiceList")]
        public List<InvoiceEntity> InvoiceList { get; set; }
    }
}
