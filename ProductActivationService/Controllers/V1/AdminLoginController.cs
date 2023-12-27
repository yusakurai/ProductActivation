using Microsoft.AspNetCore.Mvc;
using ProductActivationService.Models;
using ProductActivationService.Services;

namespace ProductActivationService.Controllers.V1
{
    /// <summary>
    /// AdminLogin コントローラー
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class AdminLoginController(ILogger<AdminLoginController> logger, IAdminLoginService service) : ControllerBase
    {
        private ILogger<AdminLoginController> Logger => logger;
        private IAdminLoginService Service => service;

        /// <summary>
        /// 管理ログイン
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AdminLoginModel>> AdminLogin(AdminLoginPostModel model)
        {
            Logger.LogInformation("Visited:AdminLogin");
            var result = await Service.AdminLogin(model);
            if (result.Status == IAdminLoginService.ServiceStatus.Unauthorized)
            {
                return Unauthorized();
            }
            return Ok(result.Model);
        }
    }
}
