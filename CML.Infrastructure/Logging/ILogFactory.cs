using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure
{
   public interface ILogFactory
    {
        ILog Create(string name);

        ILog Create(Type type);
    }
}
