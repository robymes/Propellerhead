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

        public CustomerNoteAddedEvent(string customerId, string note)
        {
            this.CustomerId = customerId;
            this.Note = note;
        }
    }
}
