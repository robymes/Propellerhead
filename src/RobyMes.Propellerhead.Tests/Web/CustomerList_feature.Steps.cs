using LightBDD.XUnit2;
using RobyMes.Propellerhead.Common.Data;
using RobyMes.Propellerhead.Data.InMemory;
using Shouldly;
using System.Threading.Tasks;

namespace RobyMes.Propellerhead.Tests.Web
{
    public partial class CustomerList_feature : FeatureFixture
    {
        private IRepository repository;

        private async Task Customers_are_available_inmemory_repository()
        {
            this.repository = new InMemoryRepository();
            await Task.CompletedTask;
        }

        private async Task Customers_are_retrieved()
        {
            var customers = await this.repository.GetCustomers();
            customers.Count.ShouldBe(20);
        }
    }
}
