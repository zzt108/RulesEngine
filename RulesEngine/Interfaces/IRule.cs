using System.Collections.Generic;

namespace RulesEngine
{
    public interface IRule
    {
        IActions Execute();
    }

    public interface IActions
    {
        IEnumerable<IAction> ActionCollection { get; }
    }

    public interface IAction
    {
        string Verb { get; }
        IEnumerable<string> Arguments { get; }
    }
}