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
        public async Task<ActionResult<IEnumerable<TokenListModel>>> GetList([FromQuery] TokenListRequest request)
        {
            Logger.LogInformation("Visited:GetList");
            var modelList = await Service.GetList(request.Sub);
            return modelList;
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="sub">sub</param>
        [HttpGet("{sub}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TokenDetailModel>> GetDetail(string sub)
        {
            var model = await Service.GetDetail(sub);
            if (model == null)
            {
                return NotFound();
            }
            return model;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="model">登録モデル</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TokenDetailModel>> Post(TokenUpdateModel model)
        {
            var result = await Service.Insert(model);
            // TODO: 戻りをクラスにしてItem1,2の指定をなくす
            if (result.Item2 == ITokenService.ServiceStatus.Conflict)
            {
                return Conflict();
            }
            return CreatedAtAction(nameof(GetDetail), new { id = result.Item1!.Sub }, result.Item1);
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
        public async Task<IActionResult> Put(string sub, [FromBody] TokenUpdateModel model)
        {
            var result = await Service.Update(sub, model);
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
        public async Task<IActionResult> Delete(string sub)
        {
            var result = await Service.Delete(sub);
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
