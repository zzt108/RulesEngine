using System;

namespace RulesEngine
{
    public class Book : Product, IBook
    {
        public Book(string name, IOwner owner) : base(name, owner)
        {
        }

        public string PackingListItem()
        {
            throw new NotImplementedException();
        }
    }
}