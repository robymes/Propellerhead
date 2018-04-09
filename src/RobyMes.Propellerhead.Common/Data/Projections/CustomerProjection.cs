using RobyMes.Propellerhead.Common.Data.Events;
using System;

namespace RobyMes.Propellerhead.Common.Data.Projections
{
    public class CustomerProjection : BaseProjection
    {
        public string CustomerId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }

        public CustomerStatus Status
        {
            get;
            set;
        }

        public CustomerProjection()
        {

        }

        public void Apply(CustomerCreatedEvent evt)
        {
            this.CustomerId = Guid.NewGuid().ToString("N").ToUpperInvariant();
            this.CreationDate = evt.CreationDate;
            this.Name = evt.Name;
            this.Status = evt.Status;
        }
    }
}
