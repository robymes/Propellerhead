using Microsoft.AspNetCore.Mvc;
using RobyMes.Propellerhead.Common.Data;
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

        [HttpPost]
        public async Task<IActionResult> GetCustomers([FromBody] CustomerListQueryParameters query)
        {
            var result = await this.repository.GetCustomers(query);
            return this.Ok(result);
        }        
    }
}