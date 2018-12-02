using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    
    [ApiController]
    public class CustomersController : ControllerBase
    {
        readonly NorthwindDbContext northwindDbContext;
        public CustomersController(NorthwindDbContext _northwindDbContext)
        {
            northwindDbContext = _northwindDbContext;
        }
        [HttpGet]
        public IActionResult GetCustomer()
        {
            return new ObjectResult(northwindDbContext.Customers);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] string id)
        {
            var customer = await northwindDbContext.Customers.FirstOrDefaultAsync(a => a.CustomerId == id.ToString());
            return Ok(customer);
        }
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customers obj )
        {
            northwindDbContext.Customers.Add(obj);
            await northwindDbContext.SaveChangesAsync();
            return CreatedAtAction("getCustomer", new { id = obj.CustomerId });
            
            
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id,[FromBody] Customers obj)
        {
            northwindDbContext.Entry(obj).State = EntityState.Modified;
            await northwindDbContext.SaveChangesAsync();
            return Ok(obj);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] string id)
        {
            var customers = await northwindDbContext.Customers.FirstOrDefaultAsync(a => a.CustomerId == id.ToString());
            northwindDbContext.Customers.Remove(customers);
            await northwindDbContext.SaveChangesAsync();
            return Ok(customers);
        }
    }

}