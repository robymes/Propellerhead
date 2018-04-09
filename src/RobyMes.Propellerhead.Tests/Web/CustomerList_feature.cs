using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;
using RobyMes.Propellerhead.Common.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: LightBddScope]

namespace RobyMes.Propellerhead.Tests.Web
{
    [FeatureDescription(
    @"In order to view initial customer list
    As an user
    I want to retrieve customers from repository")]
    public partial class CustomerList_feature : FeatureFixture
    {
        [Scenario]        
        public async Task Retrieve_initial_customer_list()
        {
            string customer1Name = "Customer 01",
                customer2Name = "Customer 02";
            await this.Runner.RunScenarioAsync(
                given => Customer_repository_is_available(),
                and => Customers_are_created(new List<string>()
                {
                    customer1Name,
                    customer2Name
                }),
                when => Customers_are_retrieved(),
                then => Customer_list_contains_items(2),
                then => Customer_list_contains_items(customer1Name, CustomerStatus.NonActive, 1),
                then => Customer_list_contains_items(customer2Name, CustomerStatus.NonActive, 1)
            );
        }
    }
}
