namespace RulesEngine
{
    using System.Collections.Generic;

    public interface IAction
    {
        string Verb { get; }
        IEnumerable<string> Arguments { get; }
    }
}