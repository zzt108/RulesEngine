namespace RulesEngine
{
    public interface IMembershipProduct : INonPhysicalProduct
    {
        IAction NotifyOwner();
    }
}