using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RobyMes.Propellerhead.Common.Data;

namespace RobyMes.Propellerhead.Data.InMemory
{
    public class InMemoryRepository : IRepository
    {
        private List<Customer> customers;

        public InMemoryRepository()
        {
            this.customers = new List<Customer>();
            for (int i = 0; i < 20; i++)
            {
                this.customers.Add(new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = $"Customer #{i}",
                    CreationDate = DateTime.Now.Date.Subtract(new TimeSpan(i * 10, 0, 0, 0)),
                    Status = (i % 2 == 0 ? CustomerStatus.Current : (i % 3 == 0 ? CustomerStatus.NonActive : CustomerStatus.Current)).ToString()
                });
            }
        }

        public async Task<IList<Customer>> GetCustomers()
        {
            return await Task.FromResult(this.customers);
        }
    }
}
