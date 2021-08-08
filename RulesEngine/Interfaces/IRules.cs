using System.Collections.Generic;

namespace RulesEngine
{
    public interface IRules
    {
        IEnumerable<IRule> RulesCollection { get; }

        IActions ExecuteAll(IPaymentItem paymentItem);
    }
}