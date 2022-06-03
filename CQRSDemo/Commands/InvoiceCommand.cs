using CQRSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public class CreateInvoiceCommand : Command
    {
        public string DOI { get; set; }
        public long Amount { get; set; }
        public long InvoiceNo { get; set; }
    }
}
