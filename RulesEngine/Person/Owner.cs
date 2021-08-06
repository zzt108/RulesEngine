namespace RulesEngine
{
    public class Owner : Person, IOwner
    {
        public Owner(string name, string email) : base(name)
        {
            Email = email;
        }

        public string Email { get; }
    }
}