namespace RulesEngine
{
    public class Person : IPerson
    {
        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}