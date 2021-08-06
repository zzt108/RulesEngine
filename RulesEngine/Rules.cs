using System.Collections.Generic;

namespace RulesEngine
{
    public class Rules : IRules
    {
        private IEnumerable<IRule> _rules;
        public Rules()
        {
            _rules = new List<IRule>(); 
        }

        public IEnumerable<IRule> RulesCollection { get; }
    }
}