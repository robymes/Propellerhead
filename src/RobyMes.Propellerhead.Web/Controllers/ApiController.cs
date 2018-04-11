using Microsoft.AspNetCore.Mvc;
using RobyMes.Propellerhead.Common.Data;
using RobyMes.Propellerhead.Web.Models;
using System;
using System.Threading.Tasks;

namespace RobyMes.Propellerhead.Web.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ApiController : Controller
    {
        private IRepository repository;

        public ApiController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Ping()
        {
            var result = await Task.FromResult(new
            {
                Timestamp = DateTime.Now
            });
            return this.Ok(result);
        }

        [HttpPost("GetCustomers")]
        public async Task<IActionResult> GetCustomers([FromBody] CustomerListQueryParameters query)
        {
            var result = await this.repository.GetCustomers(query);
            return this.Ok(result);
        }

        [HttpPost("GetCustomersOrderByName")]
        public async Task<IActionResult> GetCustomersOrderByName([FromBody] GetOrderedCustomersRequest request)
        {
            var result = await this.repository.GetCustomersOrderByName(request.Query, request.Ascending);
            return this.Ok(result);
        }

        [HttpPost("GetCustomersOrderByCreationDate")]
        public async Task<IActionResult> GetCustomersOrderByCreationDate([FromBody] GetOrderedCustomersRequest request)
        {
            var result = await this.repository.GetCustomersOrderByCreationDate(request.Query, request.Ascending);
            return this.Ok(result);
        }

        [HttpPost("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById([FromBody] GetCustomerByIdRequest request)
        {
            var result = await this.repository.GetCustomerById(request.Id);
            return this.Ok(result);
        }

        [HttpPost("NewCustomer")]
        public async Task<IActionResult> NewCustomer([FromBody] NewCustomerRequest request)
        {
            await this.repository.CreateCustomer(request.Name, CustomerStatus.NonActive);
            return this.Ok();
        }

        [HttpPost("UpdateCustomerStatus")]
        public async Task<IActionResult> UpdateCustomerStatus([FromBody] UpdateCustomerStatusRequest request)
        {
            await this.repository.UpdateCustomerStatus(request.Id, request.Status);
            return this.Ok();
        }

        [HttpPost("AddCustomerNote")]
        public async Task<IActionResult> AddCustomerNote([FromBody] AddCustomerNoteRequest request)
        {
            await this.repository.AddCustomerNote(request.Id, request.Note);
            return this.Ok();
        }
    }
}