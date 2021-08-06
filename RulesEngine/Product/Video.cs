using System;

namespace RulesEngine
{
    public class Video : Product, IVideo
    {
        public string PackingListItem()
        {
            throw new NotImplementedException();
        }

        public Video(string name, IOwner owner) : base(name, owner)
        {
        }
    }
}