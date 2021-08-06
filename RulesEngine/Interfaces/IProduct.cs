using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine
{
    interface IProduct
    {
        string Name { get; }
        IOwner Owner { get; }
    }
}
