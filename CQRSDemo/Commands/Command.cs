using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public abstract class Command
    {
        public long Id { get; set; }
    }
}
