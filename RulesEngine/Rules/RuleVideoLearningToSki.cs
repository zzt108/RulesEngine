namespace RulesEngine
{
    /// <summary>
    /// If the payment is for a book, create a duplicate packing slip for the royalty department.
    /// </summary>
    public class RuleVideoLearningToSki : IRule
    {
        public IActions Execute(IPaymentItem item)
        {
            if (item.Product is IVideo)
            {
                if (item.Product.Name == ActionConstants.LearningToSki)
                {
                    var actionItem = new ActionItem(
                        ActionConstants.GeneratePackingSlip,
                        new[] { ActionConstants.Shipping, ActionConstants.FirstAid, item.Product.Name });
                    return new Actions(new IAction[] { actionItem });
                }
                else return null;
            }
            else return null;
        }
    }
}