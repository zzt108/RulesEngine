using System;

namespace RulesEngine
{
    public class Video : Product, IVideo
    {
        public Video(string name, IOwner owner, IAgent agent) : base(name, owner)
        {
            this.Agent = agent;
        }

        public IAgent Agent { get; }
    }
}