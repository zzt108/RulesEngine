using System;
using System.Collections.Generic;

namespace RulesEngine
{
    public interface IPayment
    {
        IEnumerable<IPaymentItem> PaymentItems { get; }
        DateTime Date { get; }
    }
}