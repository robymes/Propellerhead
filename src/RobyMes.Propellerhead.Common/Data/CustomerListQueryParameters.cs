using System;

namespace RobyMes.Propellerhead.Common.Data
{
    public class CustomerListQueryParameters
    {
        public int PageSize
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public string NameFilter
        {
            get;
            set;
        }

        public DateTime? CreatioDateFilter
        {
            get;
            set;
        }

        public CustomerStatus? CustomerStatusFilter
        {
            get;
            set;
        }
    }
}
