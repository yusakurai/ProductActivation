using Microsoft.AspNetCore.Mvc;
using ProductActivationService.Models;
using ProductActivationService.Requests;
using ProductActivationService.Services;

namespace ProductActivationService.Controllers.V1
{
    /// <summary>
    /// Customer コントローラー
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [Produces("application/json")]
    public class CustomerController(ILogger<CustomerController> logger, ICustomerService service) : ControllerBase
    {
        private ILogger<CustomerController> Logger => logger;
        private ICustomerService Service => service;

        /// <summary>
        /// リスト取得
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CustomerListModel>>> GetCustomer([FromQuery] ListCustomerRequest request)
        {
            Logger.LogInformation("Visited:GetCustomer");
            var result = await Service.GetCustomers(request.Name);
            return result;
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="id">顧客ID</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDetailModel>> GetCustomer(long id)
        {
            var result = await Service.GetCustomer(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="model">顧客登録モデル</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CustomerDetailModel>> PostCustomer(CustomerUpdateModel model)
        {
            var result = await Service.InsertCustomer(model);
            // TODO: 戻りをクラスにしてItem1,2の指定をなくす
            if (result.Item2 == ICustomerService.ServiceStatus.Conflict)
            {
                return Conflict();
            }
            return CreatedAtAction(nameof(GetCustomer), new { id = result.Item1!.Id }, result.Item1);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">顧客ID</param>
        /// <param name="model">顧客更新モデル</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutCustomer(long id, [FromBody] CustomerUpdateModel model)
        {
            var result = await Service.UpdateCustomer(id, model);
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

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id">顧客ID</param>
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
