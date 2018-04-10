using RobyMes.Propellerhead.Common.Data;

namespace RobyMes.Propellerhead.Web.Models
{
    public class GetOrderedCustomersRequest
    {
        public CustomerListQueryParameters Query
        {
            get;
            set;
        }

        public bool Ascending
        {
            get;
            set;
        }
    }
}
