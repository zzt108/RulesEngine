using System.Collections.Generic;

namespace RulesEngine
{
    public class Rules : IRules
    {
        public Rules(IEnumerable<IRule> rulesCollection)
        {
            RulesCollection = rulesCollection;
        }

        public IEnumerable<IRule> RulesCollection { get; }
    }
}