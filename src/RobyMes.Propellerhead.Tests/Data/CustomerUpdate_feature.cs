using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Contextual;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;
using RobyMes.Propellerhead.Common.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobyMes.Propellerhead.Tests.Data
{
    [FeatureDescription(
    @"In order to update a customer
    As an user
    I want to change status or add a note")]
    public class CustomerUpdate_feature : FeatureFixture
    {
        [Scenario(DisplayName = "Update customer status")]        
        public async Task Update_customer_status()
        {
            var customerNames = new List<string>()
            {
                "Customer 01",
                "Customer 02"
            };            
            var status = CustomerStatus.Current;
            await this.Runner.WithContext<CustomerUpdateContext>().RunScenarioAsync(
                given => given.Customer_repository_is_available(),
                and => and.Customers_are_created(customerNames),
                and => and.Customer_id_is_retrieved(0),
                and => and.Customer_is_in_status(CustomerStatus.NonActive),
                when => when.Customer_status_updated(status),
                then => then.Customer_is_in_status(status)
            );
        }

        [Scenario(DisplayName = "Add customer notes")]
        public async Task Add_customer_notes()
        {
            var customerNames = new List<string>()
            {
                "Customer 01",
                "Customer 02"
            };
            var notes = new List<string>()
            {
                "Note 01",
                "Note 02"
            };
            await this.Runner.WithContext<CustomerUpdateContext>().RunScenarioAsync(
                given => given.Customer_repository_is_available(),
                and => and.Customers_are_created(customerNames),
                and => and.Customer_id_is_retrieved(0),
                when => when.Customer_note_added(notes[0]),
                and => and.Customer_note_added(notes[1]),
                then => then.Customer_has_notes_count(2),
                and => and.Customer_has_note(notes[0]),
                and => and.Customer_has_note(notes[1])
            );
        }
    }
}
