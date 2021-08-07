namespace RulesEngine
{
    public interface IPhysicalProduct :IProduct
    {
        IAgent Agent { get; }
    }
}