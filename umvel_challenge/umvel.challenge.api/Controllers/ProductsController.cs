using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using umvel.challenge.application.Commands.Products;
using umvel.challenge.application.Queries.Product;
using umvel.challenge.domain.Models.Products;

namespace umvel.challenge.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ApiController
    {
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery]int productId)
        {
            return Success(await ApiMediator.Send(new GetProductByProductIdQuery(productId)));
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody]ProductModel product)
        {
            return Success(await ApiMediator.Send(new CreateProductCommand(product)));
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody]ProductModel product)
        {
            return Success(await ApiMediator.Send(new UpdateProductCommand(product)));
        }
    }
}

