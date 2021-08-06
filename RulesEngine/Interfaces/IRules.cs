using System.Collections.Generic;

namespace RulesEngine
{
    interface IRules
    {
        IEnumerable<IRule> RulesCollection { get; }
    }
}