using System.Collections.Generic;

namespace RulesEngine
{
    interface IPayment
    {
        IEnumerable<IPaymentItem> Products { get; }
    }
}