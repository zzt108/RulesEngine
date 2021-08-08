namespace RulesEngine
{
    using System.Collections.Generic;

    public interface IActions
    {
        IEnumerable<IAction> ActionCollection { get; }
    }
}