namespace RulesEngine
{
    /// <summary>
    /// If the payment is an upgrade to a membership, apply the upgrade.
    /// </summary>
    public class RuleMembershipUpgrade : IRule
    {
        public IActions Execute(IPaymentItem item)
        {
            if (item.Product is IMembershipUpgrade)
            {
                var actionItem = new ActionItem(ActionConstants.MembershipUpgrade, new[] { ActionConstants.Upgrade, item.Product.Name });
                return new Actions(new IAction[] {actionItem});
            }
            else return null;
        }
    }
}