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

        ValueTask<List<TokenListModel>> GetTokens(string? sub);
        ValueTask<TokenDetailModel?> GetToken(string sub);
        ValueTask<(TokenDetailModel?, ServiceStatus)> InsertToken(TokenUpdateModel model);
        ValueTask<(TokenDetailModel?, ServiceStatus)> UpdateToken(string sub, TokenUpdateModel model);
        ValueTask<ServiceStatus> DeleteToken(string sub);
    }

}
