using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Events
{
    public class CustomerDeletedEvent : IEvent
    {
        public long CustomerId { get; set; }
    }
}
