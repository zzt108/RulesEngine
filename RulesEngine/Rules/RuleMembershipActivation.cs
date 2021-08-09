namespace RulesEngine
{

    /// <summary>
    /// If the payment is for a membership, activate that membership.
    /// </summary>
    public class RuleMembershipActivation : IRule
    {
        public IActions Execute(IPaymentItem item)
        {
            if (item.Product is IMembershipActivation)
            {
                var actionItem = new ActionItem(ActionConstants.MembershipActivation, new[] { ActionConstants.Activate, item.Product.Name });
                return new Actions(new IAction[] {actionItem});
            }
            else return null;
        }
    }
}