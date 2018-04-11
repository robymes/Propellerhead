using RobyMes.Propellerhead.Common.Data;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;

namespace RobyMes.Propellerhead.Tests.Data
{
    public sealed class CustomerUpdateContext : CustomerQueryingContext
    {
        private string customerId;

        public async Task Customer_id_is_retrieved(int customerIndex)
        {
            var queryParameters = new CustomerListQueryParameters()
            {
                PageSize = 10,
                PageIndex = 0
            };
            this.customerList = await this.repository.GetCustomers(queryParameters);
            this.customerId = this.customerList[customerIndex].Id;
        }

        public async Task Customer_status_updated(CustomerStatus status)        
        {
            await this.repository.UpdateCustomerStatus(this.customerId, status);
        }

        public async Task Customer_is_in_status(CustomerStatus status)
        {
            var customer = await this.repository.GetCustomerById(this.customerId);
            customer.ShouldNotBeNull();
            customer.Status.ShouldBe(status.ToString());
        }

        public async Task Customer_note_added(string note)
        {
            await this.repository.AddCustomerNote(this.customerId, note);
        }

        public async Task Customer_has_notes_count(int notesCount)
        {
            var customer = await this.repository.GetCustomerById(this.customerId);
            customer.ShouldNotBeNull();
            customer.Notes.Count.ShouldBe(notesCount);
        }

        public async Task Customer_has_note(string note)
        {
            var customer = await this.repository.GetCustomerById(this.customerId);
            customer.ShouldNotBeNull();
            customer.Notes.Select(n => n.Text).ToList().ShouldContain(note);
        }
    }
}
