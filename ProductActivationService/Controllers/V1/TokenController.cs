using Microsoft.AspNetCore.Mvc;
using ProductActivationService.Models;
using ProductActivationService.Requests;
using ProductActivationService.Services;

namespace ProductActivationService.Controllers.V1
{
    /// <summary>
    /// Token コントローラー
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class TokenController(ILogger<TokenController> logger, ITokenService service) : ControllerBase
    {
        private ILogger<TokenController> Logger => logger;
        private ITokenService Service => service;

        /// <summary>
        /// リスト取得
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TokenListModel>>> GetToken([FromQuery] TokenListRequest request)
        {
            Logger.LogInformation("Visited:GetToken");
            var result = await Service.GetTokens(request.Sub);
            return result;
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="sub">sub</param>
        [HttpGet("{sub}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TokenDetailModel>> GetToken(string sub)
        {
            var result = await Service.GetToken(sub);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="model">登録モデル</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TokenDetailModel>> PostToken(TokenUpdateModel model)
        {
            var result = await Service.InsertToken(model);
            // TODO: 戻りをクラスにしてItem1,2の指定をなくす
            if (result.Item2 == ITokenService.ServiceStatus.Conflict)
            {
                return Conflict();
            }
            return CreatedAtAction(nameof(GetToken), new { id = result.Item1!.Sub }, result.Item1);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sub">sub</param>
        /// <param name="model">更新モデル</param>
        [HttpPut("{sub}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutToken(string sub, [FromBody] TokenUpdateModel model)
        {
            var result = await Service.UpdateToken(sub, model);
            if (result.Item2 == ITokenService.ServiceStatus.NotFound)
            {
                return NotFound();
            }
            else if (result.Item2 == ITokenService.ServiceStatus.Conflict)
            {
                return Conflict();
            }
            return Ok(result.Item1);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sub">sub</param>
        [HttpDelete("{sub}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteToken(string sub)
        {
            var result = await Service.DeleteToken(sub);
            if (result == ITokenService.ServiceStatus.NotFound)
            {
                return NotFound();
            }
            else if (result == ITokenService.ServiceStatus.Conflict)
            {
                return Conflict();
            }
            return NoContent();
        }
    }
}
