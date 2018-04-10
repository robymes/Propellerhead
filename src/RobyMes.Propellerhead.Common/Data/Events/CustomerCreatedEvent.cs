using System;

namespace RobyMes.Propellerhead.Common.Data.Events
{
    public class CustomerCreatedEvent : BaseEvent
    {
        public string CustomerId
        {
            get;
            protected set;
        }

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

        public CustomerCreatedEvent(string id, string name, DateTime creationDate, CustomerStatus status)
        {
            this.CustomerId = id;
            this.Name = name;
            this.CreationDate = creationDate;
            this.Status = status;
        }
    }
}
