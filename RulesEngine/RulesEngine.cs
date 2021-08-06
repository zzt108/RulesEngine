using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine
{
    public class RulesEngine
    {
        private IRules _rules;
        public RulesEngine()
        {
            _rules = GetRules();
        }

        private IRules GetRules()
        {
            return new Rules();
        }

        public void Execute(IPayment payment)
        {
            throw new NotImplementedException();
        }
    }
}
