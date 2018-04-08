using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobyMes.Propellerhead.Common.Data
{
    public interface IRepository
    {
        Task<IList<Customer>> GetCustomers();
    }
}
