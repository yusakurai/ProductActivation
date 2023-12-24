using Microsoft.AspNetCore.Mvc;
using ProductActivationService.Models;
using ProductActivationService.Requests;
using ProductActivationService.Services;

namespace ProductActivationService.Controllers.V1_1
{
    /// <summary>
    /// Customer コントローラー
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.1", Deprecated = true)]
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
            Logger.LogInformation("Visited:GetCustomer v1.1");
            var modelList = await Service.GetList(request.Name);
            return modelList;
        }
    }
}
