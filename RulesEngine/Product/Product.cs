namespace RulesEngine
{
    public class Product : IProduct
    {
        protected Product(string name, IOwner owner)
        {
            Name = name;
            Owner = owner;
        }

        public string Name { get; }
        public IOwner Owner { get; }
    }
}