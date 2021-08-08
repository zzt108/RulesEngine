using System.Collections.Generic;

namespace RulesEngine
{
    public interface IRules
    {

        IActions ExecuteAll(IPaymentItem paymentItem);
    }
}