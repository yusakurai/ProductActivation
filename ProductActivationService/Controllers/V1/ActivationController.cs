using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductActivationService.Models;
using ProductActivationService.Services;

namespace ProductActivationService.Controllers.V1
{
    /// <summary>
    /// Activation コントローラー
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class ActivationController(ILogger<ActivationController> logger, IActivationService service) : ControllerBase
    {
        private ILogger<ActivationController> Logger => logger;
        private IActivationService Service => service;

        /// <summary>
        /// アクティベーション
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ActivationModel>> Activation(ActivationPostModel model)
        {
            Logger.LogInformation("Visited:Activation");
            var result = await Service.Activation(model);
            if (result.Status == IActivationService.ServiceStatus.Unauthorized)
            {
                return Unauthorized();
            }
            return Ok(result.Model);
        }
    }
}
