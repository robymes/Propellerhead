using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Contextual;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;
using RobyMes.Propellerhead.Common.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: LightBddScope]

namespace RobyMes.Propellerhead.Tests.Web
{
    [FeatureDescription(
    @"In order to query for customers
    As an user
    I want to retrieve customers from repository applying query criteria")]
    public class CustomerQuerying_feature : FeatureFixture
    {
        [Scenario(DisplayName = "Initial customer list")]        
        public async Task Retrieve_initial_customer_list()
        {
            var customerNames = new List<string>()
            {
                "Customer 01",
                "Customer 02"
            };
            var queryParameters = new CustomerListQueryParameters()
            {
                PageSize = 10,
                PageIndex = 0
            };
            await this.Runner.WithContext<CustomerQueryingContext>().RunScenarioAsync(
                given => given.Customer_repository_is_available(),
                and => and.Customers_are_created(customerNames),
                when => when.Customers_are_retrieved(queryParameters),
                then => then.Customer_list_contains_items(2),
                then => then.Customer_list_contains_items(customerNames[0], CustomerStatus.NonActive, 1),
                then => then.Customer_list_contains_items(customerNames[1], CustomerStatus.NonActive, 1)
            );
        }

        [Scenario(DisplayName = "Paged customer list")]
        public async Task Retrieve_specific_page_customer_list()
        {
            var customerCount = 23;
            var customerNames = new List<string>();
            for (int i = 0; i < customerCount; i++)
            {
                customerNames.Add($"Customer {i.ToString("00")}");
            }
            var queryParameters = new CustomerListQueryParameters()
            {
                PageSize = 10,
                PageIndex = 2
            };
            await this.Runner.WithContext<CustomerQueryingContext>().RunScenarioAsync(
                given => given.Customer_repository_is_available(),
                and => and.Customers_are_created(customerNames),
                when => when.Customers_are_retrieved(queryParameters),
                then => then.Customer_list_contains_items(3),
                then => then.Customer_list_contains_items(customerNames[customerCount - 3], CustomerStatus.NonActive, 1),
                then => then.Customer_list_contains_items(customerNames[customerCount - 2], CustomerStatus.NonActive, 1),
                then => then.Customer_list_contains_items(customerNames[customerCount - 1], CustomerStatus.NonActive, 1)
            );
        }

        [Scenario(DisplayName = "Paged customer list ordered by name descending")]
        public async Task Retrieve_specific_page_customer_list_ordered_by_name_descending()
        {
            var customerCount = 24;
            var customerNames = new List<string>();
            for (int i = 0; i < customerCount; i++)
            {
                customerNames.Add($"Customer {i.ToString("00")}");
            }
            var queryParameters = new CustomerListQueryParameters()
            {
                PageSize = 10,
                PageIndex = 0
            };
            await this.Runner.WithContext<CustomerQueryingContext>().RunScenarioAsync(
                given => given.Customer_repository_is_available(),
                and => and.Customers_are_created(customerNames),
                when => when.Customers_are_retrieved_ordered_by_name(queryParameters, false),
                then => then.Customer_list_contains_items(10),
                then => then.Customer_list_contains_item(0, customerNames[customerCount - 1], CustomerStatus.NonActive),
                then => then.Customer_list_contains_item(9, customerNames[customerCount - 10], CustomerStatus.NonActive)
            );
        }

        [Scenario(DisplayName = "Paged customer list ordered by creation date ascending")]
        public async Task Retrieve_specific_page_customer_list_ordered_by_creation_date_ascending()
        {
            var customerCount = 25;
            var customerNames = new List<string>();
            for (int i = 0; i < customerCount; i++)
            {
                customerNames.Add($"Customer {i.ToString("00")}");
            }
            var queryParameters = new CustomerListQueryParameters()
            {
                PageSize = 10,
                PageIndex = 0
            };
            await this.Runner.WithContext<CustomerQueryingContext>().RunScenarioAsync(
                given => given.Customer_repository_is_available(),
                and => and.Customers_are_created(customerNames),
                when => when.Customers_are_retrieved_ordered_by_creation_date(queryParameters, true),
                then => then.Customer_list_contains_items(10),
                then => then.Customer_list_contains_item(0, customerNames[0], CustomerStatus.NonActive),
                then => then.Customer_list_contains_item(9, customerNames[9], CustomerStatus.NonActive)
            );
        }

        [Scenario(DisplayName = "Paged customer list ordered by name descending and filtered by name")]
        public async Task Retrieve_specific_page_customer_list_ordered_by_name_descending_and_filtered_by_name()
        {
            var customerCount = 24;
            var customerNames = new List<string>();
            for (int i = 0; i < customerCount; i++)
            {
                customerNames.Add($"Customer {i.ToString("00")}");
            }
            var queryParameters = new CustomerListQueryParameters()
            {
                PageSize = 20,
                PageIndex = 0,
                NameFilter = "0"
            };
            await this.Runner.WithContext<CustomerQueryingContext>().RunScenarioAsync(
                given => given.Customer_repository_is_available(),
                and => and.Customers_are_created(customerNames),
                when => when.Customers_are_retrieved_ordered_by_name(queryParameters, false),
                then => then.Customer_list_contains_items(12),
                then => then.Customer_list_contains_item(0, customerNames[20], CustomerStatus.NonActive),
                then => then.Customer_list_contains_item(11, customerNames[0], CustomerStatus.NonActive)
            );
        }

        [Scenario(DisplayName = "Paged customer list filtered by status and name")]
        public async Task Retrieve_specific_page_customer_list_filtered_by_status_and_name()
        {
            var customerCount = 24;
            var customerNames = new List<string>();
            for (int i = 0; i < customerCount; i++)
            {
                customerNames.Add($"Customer {i.ToString("00")}");
            }
            var queryParameters = new CustomerListQueryParameters()
            {
                PageSize = 20,
                PageIndex = 0,
                NameFilter = "0",
                CustomerStatusFilter = CustomerStatus.Current
            };
            await this.Runner.WithContext<CustomerQueryingContext>().RunScenarioAsync(
                given => given.Customer_repository_is_available(),
                and => and.Customers_are_created(customerNames),
                when => when.Customers_are_retrieved_ordered_by_name(queryParameters, false),
                then => then.Customer_list_contains_items(0)
            );
        }

        [Scenario(DisplayName = "Customer by Id")]
        public async Task Retrieve_customer_by_id()
        {
            var customerNames = new List<string>()
            {
                "Customer 01",
                "Customer 02"
            };
            var queryParameters = new CustomerListQueryParameters()
            {
                PageSize = 20,
                PageIndex = 0
            };
            await this.Runner.WithContext<CustomerQueryingContext>().RunScenarioAsync(
                given => given.Customer_repository_is_available(),
                and => and.Customers_are_created(customerNames),
                and => and.Customers_are_retrieved(queryParameters),
                when => when.Customer_are_retrieved_by_id(0),
                then => then.Customer_list_contains_items(1)
            );
        }
    }
}
