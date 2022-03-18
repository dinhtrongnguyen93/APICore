using ApiCore.Models.DAL.IService;
using ApiCore.Models.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        [Route("customer-detail")]
        public async Task<IActionResult> Get(int id)
        {
            Customer customer = await _customerService.GetCustomer(id);
            return Ok(customer);
        }
    }
}
