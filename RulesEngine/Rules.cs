using System.Collections.Generic;

namespace RulesEngine
{
    using System.Linq;

    public class Rules : IRules
    {
        public Rules(IEnumerable<IRule> rulesCollection)
        {
            RulesCollection = rulesCollection;
        }

        private IEnumerable<IRule> RulesCollection { get; }

        public virtual IActions ExecuteAll(IPaymentItem paymentItem)
        {
            var actionCollection = new List<IAction>();
            foreach (var rule in this.RulesCollection)
            {
                var actions = rule.Execute(paymentItem);
                actionCollection.AddRange(actions.ActionCollection);
            } 
            return new Actions(actionCollection);
        }
    }
}