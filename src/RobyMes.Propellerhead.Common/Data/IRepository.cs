using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobyMes.Propellerhead.Common.Data
{
    public interface IRepository : IDisposable
    {
        Task<IList<Customer>> GetCustomers(CustomerListQueryParameters queryParameters);

        Task<IList<Customer>> GetCustomersOrderByName(CustomerListQueryParameters queryParameters, bool ascending);

        Task<IList<Customer>> GetCustomersOrderByCreationDate(CustomerListQueryParameters queryParameters, bool ascending);

        Task CreateCustomer(string name, CustomerStatus status);
    }
}
