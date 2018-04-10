using System;
using System.Collections.Generic;

namespace RobyMes.Propellerhead.Common.Data
{
    public class Customer
    {
        public string Id
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

        public string Status
        {
            get;
            set;
        }

        public IList<string> Notes
        {
            get;
            set;
        }

        public Customer()
        {
            this.Notes = new List<string>();
        }
    }
}
