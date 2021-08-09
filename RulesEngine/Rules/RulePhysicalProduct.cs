namespace RulesEngine
{
    /// <summary>
    /// If the payment is for a physical product, generate a packing slip for shipping.
    /// </summary>
    public class RulePhysicalProduct : IRule
    {
        public IActions Execute(IPaymentItem item)
        {
            if (item.Product is IPhysicalProduct)
            {
                var actionItem = new ActionItem(ActionConstants.GeneratePackingSlip, new[] { ActionConstants.Shipping, item.Product.Name });
                return new Actions(new IAction[] {actionItem});
            }
            else return null;
        }
    }
}