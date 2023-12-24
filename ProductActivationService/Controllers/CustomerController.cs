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
using ProductActivationService.Models;

namespace ProductActivationService.Controllers
{
  /// <summary>
  /// Customer コントローラー
  /// </summary>
  [ApiVersion("1")]
  [ApiVersion("1.1")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  [Produces("application/json")]
  public class CustomerController(ILogger<CustomerController> logger, ICustomerService service) : ControllerBase
  {
    private ILogger<CustomerController> Logger => logger;
    private ICustomerService Service => service;

    // GET: api/Customer
    /// <summary>
    /// リスト取得
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomer([FromQuery] ListCustomerRequest request)
    {
      Logger.LogInformation("Visited:GetCustomer");
      var result = await Service.GetCustomers(request.Name);
      return result;
    }

    // GET: api/v1.1/Customer
    /// <summary>
    /// リスト取得
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [MapToApiVersion("1.1")] // Apiバージョンを上書き
    public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomerV1_1([FromQuery] ListCustomerRequest request)
    {
      Logger.LogInformation("Visited:GetCustomer v1.1");
      var result = await Service.GetCustomers(request.Name);
      return result;
    }

    // GET: api/Customer/5
    /// <summary>
    /// 取得
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// <summary>
    /// 登録
    /// </summary>
    /// <param name="value"></param>
    /// <returns>A newly created Customer</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/Customer
    ///     {
    ///        "id": 1,
    ///        "name": "Item #1",
    ///        "isComplete": true
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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
    /// <summary>
    /// 削除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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
