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
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class CustomerController(ILogger<CustomerController> logger, ICustomerService service) : ControllerBase
    {
        private ILogger<CustomerController> Logger => logger;
        private ICustomerService Service => service;

        /// <summary>
        /// リスト取得
        /// </summary>
        /// <param name="request">リクエスト</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CustomerListModel>>> GetList([FromQuery] CustomerListRequest request)
        {
            Logger.LogInformation("Visited:GetList");
            var modelList = await Service.GetList(request.Name);
            return modelList;
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="id">ID</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDetailModel>> GetDetail(long id)
        {
            var model = await Service.GetDetail(id);
            if (model == null)
            {
                return NotFound();
            }
            return model;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="model">更新モデル</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CustomerDetailModel>> Post(CustomerUpdateModel model)
        {
            var result = await Service.Insert(model);
            if (result.Status == ICustomerService.ServiceStatus.Conflict)
            {
                return Conflict();
            }
            return CreatedAtAction(nameof(GetDetail), new { id = result.DetailModel!.Id }, result.DetailModel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="model">更新モデル</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Put(long id, [FromBody] CustomerUpdateModel model)
        {
            var result = await Service.Update(id, model);
            if (result.Status == ICustomerService.ServiceStatus.NotFound)
            {
                return NotFound();
            }
            else if (result.Status == ICustomerService.ServiceStatus.Conflict)
            {
                return Conflict();
            }
            return Ok(result.DetailModel);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id">ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await Service.Delete(id);
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
