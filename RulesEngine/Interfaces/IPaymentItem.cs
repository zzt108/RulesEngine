namespace RulesEngine
{
    public interface IPaymentItem
    {
        IProduct Product { get; }
        double Amount { get; }
    }
}