namespace RulesEngine
{
    public class Agent : Person, IAgent
    {
        public Agent(string name, double commission) : base(name)
        {
            Commission = commission;
        }

        public double Commission { get; }
    }
}