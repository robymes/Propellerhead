using RobyMes.Propellerhead.Common.Data;

namespace RobyMes.Propellerhead.Web.Models
{
    public class UpdateCustomerStatusRequest
    {
        public string Id
        {
            get;
            set;
        }

        public CustomerStatus Status
        {
            get;
            set;
        }
    }
}
