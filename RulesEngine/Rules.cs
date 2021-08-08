using System.Collections.Generic;

namespace RulesEngine
{
    using System.Linq;

    public class Rules : IRules
    {

        private IEnumerable<IRule> RuleCollection { get; }

        public virtual IActions ExecuteAll(IPaymentItem paymentItem)
        {
            var actionCollection = new List<IAction>();
            foreach (var rule in this.RuleCollection)
            {
                var actions = rule.Execute(paymentItem);
                actionCollection.AddRange(actions.ActionCollection);
            } 
            return new Actions(actionCollection);
        }
    }
}