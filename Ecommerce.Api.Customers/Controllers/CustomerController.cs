using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController:ControllerBase
    {
        private readonly ICustomerProvider customerProvider;
        public CustomerController(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerAsync()
        {
            var result = await customerProvider.GetCustomerAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductGetCustomerAsyncAync(int id)
        {
            var result = await customerProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }
    }
}
