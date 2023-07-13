using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using umvel.challenge.application.Commands.Customer;
using umvel.challenge.application.Commands.Customers;
using umvel.challenge.application.Queries.Customer;
using umvel.challenge.domain.Models.Customers;

namespace umvel.challenge.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ApiController
    {
        [HttpGet("GetClientById")]
        public async Task<IActionResult> GetClientById([FromQuery]int clientId)
        {
            return Success(await ApiMediator.Send(new GetCustomerByCustomerIdQuery(clientId)));
        }

        [HttpPost("CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody]CustomerModel customer)
        {
            return Success(await ApiMediator.Send(new CreateCustomerCommand(customer)));
        }

        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody]CustomerModel customer)
        {
            return Success(await ApiMediator.Send(new UpdateCustomerCommand(customer)));
        }
    }
}

