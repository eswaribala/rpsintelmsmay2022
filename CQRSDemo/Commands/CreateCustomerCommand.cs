using CQRSDemo.Events;
using CQRSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public class CreateCustomerCommand : Command
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public long MobileNo { get; set; }
        public List<CreateInvoiceCommand> InvoiceList { get; set; }
        public CustomerCreatedEvent ToCustomerEvent(long id)
        {
            return new CustomerCreatedEvent
            {
                CustomerId = id,
                CustomerName = this.CustomerName,
                Email = this.Email,
                MobileNo = this.MobileNo,
                InvoiceList = this.InvoiceList.Select(invoice => 
                new InvoiceCreatedEvent {
                    Amount= invoice.Amount,
                    DOI = invoice.DOI,
                    InvoiceNo=invoice.InvoiceNo
                }).ToList()
            };
        }
        public Customer ToCustomerRecord()
        {
            return new Customer
            {
                CustomerName = this.CustomerName,
                Email = this.Email,
                MobileNo = this.MobileNo,
                InvoiceList= this.InvoiceList.Select
                (invoice => new Invoice { Amount = invoice.Amount,
                    DOI = invoice.DOI,
                    InvoiceNo=invoice.InvoiceNo
                }).ToList()
            };
        }
    }
}
