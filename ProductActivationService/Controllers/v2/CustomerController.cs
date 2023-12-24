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

namespace ProductActivationService.Controllers.V2
{
  /// <summary>
  /// Customer コントローラー
  /// </summary>
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("2.0")]
  [Produces("application/json")]
  public class CustomerController(ILogger<CustomerController> logger, ICustomerService service) : ControllerBase
  {
    private ILogger<CustomerController> Logger => logger;
    private ICustomerService Service => service;

    // GET: api/v2/Customer
    /// <summary>
    /// リスト取得
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomerV2([FromQuery] ListCustomerRequest request)
    {
      Logger.LogInformation("Visited:GetCustomer v2");
      var result = await Service.GetCustomers(request.Name);
      return result;
    }
  }
}