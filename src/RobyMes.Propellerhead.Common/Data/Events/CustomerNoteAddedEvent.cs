using System;

namespace RobyMes.Propellerhead.Common.Data.Events
{
    public class CustomerNoteAddedEvent : BaseEvent
    {
        public string CustomerId
        {
            get;
            protected set;
        }

        public string Note
        {
            get;
            protected set;
        }

        public DateTime Timestamp
        {
            get;
            protected set;
        }

        public CustomerNoteAddedEvent(string customerId, string note, DateTime timestamp)
        {
            this.CustomerId = customerId;
            this.Note = note;
            this.Timestamp = timestamp;
        }
    }
}
