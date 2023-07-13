using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using umvel.challenge.application.Commands.Sales;
using umvel.challenge.application.Queries.Sale;
using umvel.challenge.domain.Models.Sales;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace umvel.challenge.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ApiController
    {
        [HttpPost("CreateSale")]
        public async Task<IActionResult> CreateSale([FromBody]SaleModel sale)
        {
            return Success(await ApiMediator.Send(new CreateSaleCommand(sale)));
        }

        [HttpGet("GetSalesByDateRange")]
        public async Task<IActionResult> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            return Success(await ApiMediator.Send(new GetSalesByDateRangeQuery(startDate, endDate)));
        }
    }
}

