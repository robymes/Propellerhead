using RobyMes.Propellerhead.Common.Data.Events;
using System;
using System.Collections.Generic;

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

        public IList<string> Notes
        {
            get;
            set;
        }

        public CustomerProjection()
        {
            this.Notes = new List<string>();
        }

        public void Apply(CustomerCreatedEvent evt)
        {
            this.CustomerId = evt.CustomerId;
            this.CreationDate = evt.CreationDate;
            this.Name = evt.Name;
            this.Status = evt.Status;
        }

        public void Apply(CustomerStatusUpdatedEvent evt)
        {
            this.Status = evt.Status;
        }

        public void Apply(CustomerNoteAddedEvent evt)
        {
            this.Notes.Add(evt.Note);
        }
    }
}
