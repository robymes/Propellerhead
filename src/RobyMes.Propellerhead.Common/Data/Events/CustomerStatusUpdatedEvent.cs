namespace RobyMes.Propellerhead.Common.Data.Events
{
    public class CustomerStatusUpdatedEvent : BaseEvent
    {
        public string CustomerId
        {
            get;
            protected set;
        }

        public CustomerStatus Status
        {
            get;
            protected set;
        }

        public CustomerStatusUpdatedEvent(string customerId, CustomerStatus status)
        {
            this.CustomerId = customerId;
            this.Status = status;
        }
    }
}
