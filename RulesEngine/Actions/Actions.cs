namespace RulesEngine
{
    using System.Collections.Generic;

    public class Actions : IActions
    {
        public Actions(IEnumerable<IAction> actionCollection)
        {
            this.ActionCollection = actionCollection;
        }

        public IEnumerable<IAction> ActionCollection { get; }
    }
}
