using ProductActivationService.Models;

namespace ProductActivationService.Services
{
    /// <summary>
    /// トークンサービス インターフェイス
    /// </summary>
    public interface ITokenService
    {
        enum ServiceStatus
        {
            Ok,
            NotFound,
            Conflict,
        }

        ValueTask<List<TokenListModel>> GetList(string? sub);
        ValueTask<TokenDetailModel?> GetDetail(string sub);
        ValueTask<(TokenDetailModel?, ServiceStatus)> Insert(TokenUpdateModel model);
        ValueTask<(TokenDetailModel?, ServiceStatus)> Update(string sub, TokenUpdateModel model);
        ValueTask<ServiceStatus> Delete(string sub);
    }

}
