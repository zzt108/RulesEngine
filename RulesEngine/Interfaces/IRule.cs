namespace RulesEngine
{
    public interface IRule
    {
        IActions Execute(IPaymentItem item);
    }
}