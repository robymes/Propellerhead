using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using RobyMes.Propellerhead.Common.Configuration;
using RobyMes.Propellerhead.Common.Data;
using RobyMes.Propellerhead.Common.Data.Events;
using RobyMes.Propellerhead.Common.Data.Projections;

namespace RobyMes.Propellerhead.Data.Marten
{
    public sealed class MartenRepository : IRepository
    {
        private IDocumentStore documentStore;
        private IDocumentSession session;

        public MartenRepository(IConfigurationProvider configurationProvider)
        {
            this.documentStore = new DocumentStoreWrapper(configurationProvider);
            this.session = this.documentStore.OpenSession();
        }        

        public async Task<IList<Customer>> GetCustomers()
        {
            var customers = await
                this.session.Query<CustomerProjection>()
                .Take(10)
                .ToListAsync();
            return
                customers
                .Select(c => new Customer()
                {
                    CreationDate = c.CreationDate,
                    Id = c.CustomerId,
                    Name = c.Name,
                    Status = c.Status.ToString()
                })
                .ToList();
                
        }

        public async Task CreateCustomer(string name, CustomerStatus status)
        {
            var customerCreatedEvent = new CustomerCreatedEvent(name, DateTime.Now.Date, status);
            this.session.Events.StartStream<CustomerProjection>(Guid.NewGuid(), customerCreatedEvent);
            await this.session.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (this.session != null)
            {
                this.session.Dispose();
            }
        }
    }
}
