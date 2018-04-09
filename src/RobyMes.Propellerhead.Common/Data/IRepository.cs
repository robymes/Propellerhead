using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobyMes.Propellerhead.Common.Data
{
    public interface IRepository : IDisposable
    {
        Task<IList<Customer>> GetCustomers();

        Task CreateCustomer(string name, CustomerStatus status);
    }
}
