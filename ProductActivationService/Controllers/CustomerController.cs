using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;
using ProductActivationService.Models;

namespace ProductActivationService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController(MainContext context) : ControllerBase
  {
    private MainContext Context => context;

    // GET: api/Customer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
    {
      // return await Context.Customer.ToListAsync();
      // TODO: 本来はDBから取得するが、サンプルなので固定値を返す
      await Task.Delay(0);
      var customers = Enumerable.Range(1, 5).Select(index =>
      {
        var customer1 = new Customer()
        {
          Id = 1,
          Name = "hoge"
        };
        return customer1;
      })
          .ToArray();
      return customers;
    }

    // GET: api/Customer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(long id)
    {
      var customer = await Context.Customer.FindAsync(id);

      if (customer == null)
      {
        return NotFound();
      }

      return customer;
    }

    // PUT: api/Customer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(long id, Customer customer)
    {
      if (id != customer.Id)
      {
        return BadRequest();
      }

      Context.Entry(customer).State = EntityState.Modified;

      try
      {
        await Context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CustomerExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Customer
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
    {
      Context.Customer.Add(customer);
      await Context.SaveChangesAsync();

      return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
    }

    // DELETE: api/Customer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(long id)
    {
      var customer = await Context.Customer.FindAsync(id);
      if (customer == null)
      {
        return NotFound();
      }

      Context.Customer.Remove(customer);
      await Context.SaveChangesAsync();

      return NoContent();
    }

    private bool CustomerExists(long id)
    {
      return Context.Customer.Any(e => e.Id == id);
    }
  }
}
