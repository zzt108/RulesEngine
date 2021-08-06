using System;
using System.Collections.Generic;

namespace RulesEngine
{
    public class Payment : IPayment
    {
        public Payment(DateTime date, IEnumerable<IPaymentItem> paymentItems)
        {
            Date = date;
            PaymentItems = paymentItems;
        }

        public IEnumerable<IPaymentItem> PaymentItems { get; }
        public DateTime Date { get; }
    }
}