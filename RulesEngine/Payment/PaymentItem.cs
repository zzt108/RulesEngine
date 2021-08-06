namespace RulesEngine
{
    public class PaymentItem : IPaymentItem
    {
        public PaymentItem(IProduct product, double amount)
        {
            Product = product;
            Amount = amount;
        }

        public IProduct Product { get; }
        public double Amount { get; }
    }
}