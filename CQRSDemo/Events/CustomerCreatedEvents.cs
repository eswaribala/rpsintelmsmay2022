using CQRSDemo.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Events
{
    public class CustomerCreatedEvent : IEvent
    {
        public long CustomerId { get; set; }
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public long MobileNo { get; set; }
        public List<InvoiceCreatedEvent> InvoiceList { get; set; }
        public CustomerEntity ToCustomerEntity()
        {
            return new CustomerEntity
            {
                CustomerId = this.CustomerId,
                Email = this.Email,
                CustomerName = this.CustomerName,
                MobileNo = this.MobileNo,
               InvoiceList = this.InvoiceList.Select(invoice => new InvoiceEntity
                {
                    DOI = invoice.DOI,
                    Amount = invoice.Amount,
                    InvoiceNo = invoice.InvoiceNo
                }).ToList()
            };
        }
    }
}
