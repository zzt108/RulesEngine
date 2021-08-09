namespace RulesEngine
{
    /// <summary>
    /// If the payment is for a book, create a duplicate packing slip for the royalty department.
    /// </summary>
    public class RuleBook : IRule
    {
        public IActions Execute(IPaymentItem item)
        {
            if (item.Product is IBook)
            {
                var actionItem = new ActionItem(ActionConstants.GeneratePackingSlip, new[] { ActionConstants.ForRoyaltyDepartment, item.Product.Name });
                return new Actions(new IAction[] {actionItem});
            }
            else return null;
        }
    }
}