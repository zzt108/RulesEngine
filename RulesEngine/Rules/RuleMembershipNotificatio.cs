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
                string verb = string.Empty;
                if (item.Product is IMembershipActivation)
                {
                    verb = ActionConstants.Activate;
                }
                if (item.Product is IMembershipUpgrade)
                {
                    verb = ActionConstants.Upgrade;
                }
                var actionItem = new ActionItem(ActionConstants.MembershipNotification, new[] { verb, item.Product.Owner.Email });
                return new Actions(new IAction[] {actionItem});
            }
            else return null;
        }
    }
}