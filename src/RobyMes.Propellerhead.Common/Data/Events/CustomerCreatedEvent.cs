using System;

namespace RobyMes.Propellerhead.Common.Data.Events
{
    public class CustomerCreatedEvent : BaseEvent
    {
        public string Name
        {
            get;
            protected set;
        }

        public DateTime CreationDate
        {
            get;
            protected set;
        }

        public CustomerStatus Status
        {
            get;
            protected set;
        }

        public CustomerCreatedEvent(string name, DateTime creationDate, CustomerStatus status)
        {
            this.Name = name;
            this.CreationDate = creationDate;
            this.Status = status;
        }
    }
}
