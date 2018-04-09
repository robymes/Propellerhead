using LightBDD.XUnit2;
using Moq;
using RobyMes.Propellerhead.Common.Configuration;
using RobyMes.Propellerhead.Common.Data;
using RobyMes.Propellerhead.Data.Marten;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobyMes.Propellerhead.Tests.Web
{
    public partial class CustomerList_feature : FeatureFixture
    {
        private const string CONNECTION_STRING = "host=localhost;database=event_store;password=robymes.ptt;username=postgres;";
        private IRepository repository;
        private IList<Customer> customerList;

        private async Task Customer_repository_is_available()
        {
            var configurationProviderMock = new Mock<IConfigurationProvider>();
            configurationProviderMock.SetupProperty(cf => cf.DocumentStoreConnectionString, CONNECTION_STRING);
            configurationProviderMock.SetupProperty(cf => cf.DocumentStoreSchemaName, 
                $"{this.GetType().Name}_{DateTime.Now.ToString("yyMMddHHmmss")}");
            this.repository = new MartenRepository(configurationProviderMock.Object);
            this.repository.ShouldNotBeNull();
            await Task.CompletedTask;
        }

        private async Task Customers_are_created(IEnumerable<string> names)
        {
            foreach (var name in names)
            {
                await this.repository.CreateCustomer(name, CustomerStatus.NonActive);
            }            
        }

        private async Task Customers_are_retrieved()
        {
            this.customerList = await this.repository.GetCustomers();           
        }

        private async Task Customer_list_contains_items(int count)
        {
            this.customerList.Count.ShouldBe(count);
            await Task.CompletedTask;
        }

        private async Task Customer_list_contains_items(string name, CustomerStatus status, int count)
        {
            var customers =
                this.customerList
                .Where(c => (c.Name == name) && (c.Status == status.ToString()))
                .ToList();
            customers.Count.ShouldBe(count);
            await Task.CompletedTask;
        }
    }
}
