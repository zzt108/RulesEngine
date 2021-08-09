namespace RulesEngine
{
    /// <summary>
    /// If the payment is for a membership or upgrade, e-mail the owner and inform them of the activation/upgrade.
    /// </summary>
    public class RuleMembershipNotificatio : IRule
    {
        public IActions Execute(IPaymentItem item)
        {
            if (item.Product is IMembershipProduct)
            {
                var actionItem = new ActionItem(ActionConstants.MembershipNotification, new[] { ActionConstants.MembershipNotification, item.Product.Name, item.Product.Owner.Name });
                return new Actions(new IAction[] {actionItem});
            }
            else return null;
        }
    }
}