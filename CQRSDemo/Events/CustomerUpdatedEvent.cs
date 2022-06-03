using CQRSDemo.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Events
{
    public class CustomerUpdatedEvent : IEvent
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long MobileNo { get; set; }
        public string Email { get; set; }
        public List<InvoiceCreatedEvent> InvoiceList{ get; set; }
        public CustomerEntity ToCustomerEntity(CustomerEntity entity)
        {
            return new CustomerEntity
            {
                CustomerId = this.CustomerId,
                Email = entity.Email,
                CustomerName = entity.CustomerName.Equals(this.CustomerName) ? entity.CustomerName : this.CustomerName,
                MobileNo = entity.MobileNo.Equals(this.MobileNo) ? entity.MobileNo : this.MobileNo,
                InvoiceList = GetNewOnes(entity.InvoiceList)
                .Select(invoice => new InvoiceEntity { 
                    DOI = invoice.DOI, 
                    Amount = invoice.Amount }).ToList()
            };
        }
        private List<InvoiceEntity> GetNewOnes(List<InvoiceEntity> InvoiceList)
        {
            return InvoiceList.Where(a => !this.InvoiceList.Any(x => x.InvoiceNo == a.InvoiceNo
                && x.DOI == a.DOI
                && x.Amount == a.Amount)).ToList<InvoiceEntity>();
        }
    }
}
