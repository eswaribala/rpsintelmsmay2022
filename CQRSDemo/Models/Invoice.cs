using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSDemo.Models
{
    public class Invoice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long InvoiceNo { get; set; }
        public string DOI { get; set; }

        public long Amount { get; set; }

        [ForeignKey("Customer")]
       
        public long CustomerId { set; get; }
        public Customer Customer { set; get; }

    }
}
