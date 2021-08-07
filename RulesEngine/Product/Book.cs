using System;

namespace RulesEngine
{
    public class Book : Product, IBook
    {
        public Book(string name, IOwner owner, IAgent agent) : base(name, owner)
        {
            this.Agent = agent;
        }

        public IAgent Agent { get; }
    }
}