using System;

namespace RobyMes.Propellerhead.Common.Data.Events
{
    public abstract class BaseEvent : IEvent
    {
        public Guid Id
        {
            get;
            protected set;
        }

        public BaseEvent()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
