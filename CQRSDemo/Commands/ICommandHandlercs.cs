using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public interface ICommandHandler<T> where T : Command
    {
        void Execute(T command);
    }
}
