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
        ValueTask<TokenServiceResult> Insert(TokenUpdateModel model);
        ValueTask<TokenServiceResult> Update(string sub, TokenUpdateModel model);
        ValueTask<ServiceStatus> Delete(string sub);
    }

    /// <summary>
    /// トークンサービス 結果クラス
    /// </summary>
    public class TokenServiceResult(TokenDetailModel? detailModel, ITokenService.ServiceStatus status)
    {
        public TokenDetailModel? DetailModel => detailModel;
        public ITokenService.ServiceStatus Status => status;
    }
}
