namespace RulesEngine
{
    using System.Collections.Generic;

    public class ActionItem : IAction
    {
        public ActionItem(string verb, IEnumerable<string> arguments)
        {
            this.Verb = verb;
            this.Arguments = arguments;
        }

        public string Verb { get; }

        public IEnumerable<string> Arguments { get; }
    }
}