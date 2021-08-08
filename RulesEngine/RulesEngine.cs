using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine
{
    using System.Collections.Generic;

    public class RulesEngine
    {
        private IRules _rules;

        public RulesEngine(IRules rules) => _rules = rules;

        public virtual IActions Execute(IPayment payment)
        {
            var actionCollection = new List<IAction>();
            foreach (var item in payment.PaymentItems)
            {
                var actions = this._rules.ExecuteAll(item);
                actionCollection.AddRange(actions.ActionCollection);
            }
            return new Actions(actionCollection);
        }
    }
}
