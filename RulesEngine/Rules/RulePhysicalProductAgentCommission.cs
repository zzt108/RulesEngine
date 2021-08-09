namespace RulesEngine
{
    using System.Globalization;

    /// <summary>
    /// If the payment is for a physical product or a book, generate a commission payment to the agent.
    /// </summary>
    public class RulePhysicalProductAgentCommission : IRule
    {
        public IActions Execute(IPaymentItem item)
        {
            if (item.Product is IPhysicalProduct)
            {
                var product = item.Product as IPhysicalProduct;
                var actionItem = new ActionItem(
                    ActionConstants.CommissionPayment,
                    new[] { product.Agent.Name, product.Agent.Commission.ToString(CultureInfo.InvariantCulture), item.Product.Name });
                return new Actions(new IAction[] {actionItem});
            }
            else return null;
        }
    }
}