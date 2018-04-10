using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Marten;
using Marten.Linq;
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

        private IList<Customer> GetCustomersFromProjection(IReadOnlyList<CustomerProjection> customerProjection)
        {
            return
                customerProjection
                .Select(c => new Customer()
                {
                    CreationDate = c.CreationDate,
                    Id = c.CustomerId,
                    Name = c.Name,
                    Status = c.Status.ToString()
                })
                .ToList();
        }

        private IQueryable<CustomerProjection> BuildFilters(IQueryable<CustomerProjection> query, CustomerListQueryParameters queryParameters)
        {
            if (string.IsNullOrEmpty(queryParameters.NameFilter) == false)
            {
                query =
                    query
                    .Where(c => c.Name.ToLower().Contains(queryParameters.NameFilter.ToLower()) == true);
            }
            if (queryParameters.CreatioDateFilter != null)
            {
                var maxCreationDateFilter = queryParameters.CreatioDateFilter.Value.Date.AddDays(1);
                var minCreationDateFilter = queryParameters.CreatioDateFilter.Value.Date;
                query =
                    query
                    .Where(c => (c.CreationDate >= minCreationDateFilter) &&  (c.CreationDate < maxCreationDateFilter));
            }
            if (queryParameters.CustomerStatusFilter != null)
            {
                query =
                    query
                    .Where(c => c.Status == queryParameters.CustomerStatusFilter);
            }
            return query;
        }

        public async Task<IList<Customer>> GetCustomers(CustomerListQueryParameters queryParameters)
        {
            var customers = await
                this.BuildFilters(this.session.Query<CustomerProjection>(), queryParameters)                
                .Skip(queryParameters.PageIndex * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();
            return this.GetCustomersFromProjection(customers);                
        }

        public async Task<IList<Customer>> GetCustomersOrderByName(CustomerListQueryParameters queryParameters, bool ascending)
        {
            var query =
                this.BuildFilters(this.session.Query<CustomerProjection>(), queryParameters)
                .OrderBy(c => c.Name);
            if (ascending == false)
            {
                query =
                    this.session.Query<CustomerProjection>()
                    .OrderByDescending(c => c.Name);
            }
            var customers = await
                query
                .Skip(queryParameters.PageIndex * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();
            return this.GetCustomersFromProjection(customers);
        }

        public async Task<IList<Customer>> GetCustomersOrderByCreationDate(CustomerListQueryParameters queryParameters, bool ascending)
        {
            var query =
                this.BuildFilters(this.session.Query<CustomerProjection>(), queryParameters)
                .OrderBy(c => c.CreationDate);
            if (ascending == false)
            {
                query =
                    this.session.Query<CustomerProjection>()
                    .OrderByDescending(c => c.CreationDate);
            }
            var customers = await
                query
                .Skip(queryParameters.PageIndex * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();
            return this.GetCustomersFromProjection(customers);
        }

        public async Task CreateCustomer(string name, CustomerStatus status)
        {
            var customerCreatedEvent = new CustomerCreatedEvent(name, DateTime.Now, status);
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
