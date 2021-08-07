namespace RulesEngine
{
    public interface IMembershipActivation : IMembershipProduct
    {
        IMembership Activate();
    }
}