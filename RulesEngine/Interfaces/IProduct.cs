using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine
{
    public interface IProduct
    {
        string Name { get; }
        IOwner Owner { get; }
    }
}
