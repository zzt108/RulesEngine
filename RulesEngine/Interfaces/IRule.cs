namespace RulesEngine
{
    interface IRule
    {
        IActions Execute(IPaymentItem item);
    }
}