using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine
{
    public class RulesEngine
    {
        private IRules _rules;

        public RulesEngine(IRules rules) => _rules = rules;

        public virtual IActions Execute(IPayment payment)
        {
            throw new NotImplementedException();
        }
    }
}
