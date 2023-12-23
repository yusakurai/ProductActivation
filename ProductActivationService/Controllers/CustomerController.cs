using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;
using ProductActivationService.Entities;
using ProductActivationService.Services;
using ProductActivationService.Requests;
using ProductActivationService.Model;

namespace ProductActivationService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController(ILogger<CustomerController> logger, ICustomerService service) : ControllerBase
  {
    private ILogger<CustomerController> Logger => logger;
    private ICustomerService Service => service;

    // GET: api/Customer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomer([FromQuery] ListCustomerRequest request)
    {
      Logger.LogInformation("Visited:GetCustomer");
      var result = await Service.GetCustomers(request.Name);
      return result;
    }

    // GET: api/Customer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerModel>> GetCustomer(long id)
    {
      var result = await Service.GetCustomer(id);
      if (result == null)
      {
        return NotFound();
      }
      return result;
    }

    // POST: api/Customer
    [HttpPost]
    public async Task<ActionResult<CustomerModel>> PostCustomer(InsertCustomerModel value)
    {
      var result = await Service.InsertCustomer(value);
      // TODO: 戻りをクラスにしてItem1,2の指定をなくす
      if (result.Item2 == ICustomerService.ServiceStatus.Conflict)
      {
        return Conflict();
      }
      return CreatedAtAction(nameof(GetCustomer), new { id = result.Item1!.Id }, result.Item1);
    }

    // PUT: api/Customer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(long id, [FromBody] UpdateCustomerModel value)
    {
      var result = await Service.UpdateCustomer(id, value);
      if (result.Item2 == ICustomerService.ServiceStatus.NotFound)
      {
        return NotFound();
      }
      else if (result.Item2 == ICustomerService.ServiceStatus.Conflict)
      {
        return Conflict();
      }
      return Ok(result.Item1);
    }

    // DELETE: api/Customer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(long id)
    {
      var result = await Service.DeleteCustomer(id);
      if (result == ICustomerService.ServiceStatus.NotFound)
      {
        return NotFound();
      }
      else if (result == ICustomerService.ServiceStatus.Conflict)
      {
        return Conflict();
      }
      return NoContent();
    }
  }
}
