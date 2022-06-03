using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Models.Mongo
{
    public partial class InvoiceEntity
    {
        [BsonElement("InvoiceNo")]
        public long InvoiceNo { get; set; }
        [BsonElement("DOI")]
        public String DOI { get; set; }
        [BsonElement("Amount")]
        public long  Amount { get; set; }
      
    }
}
