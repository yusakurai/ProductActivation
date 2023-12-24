using ProductActivationService.Entities;

namespace ProductActivationService.Repositories
{
    /// <summary>
    /// トークンリポジトリー インターフェイス
    /// </summary>
    public interface ITokenRepository
    {
        ValueTask<IEnumerable<TokenEntity>> GetTokens(string? sub = null);
        ValueTask<TokenEntity?> GetTokenBySub(string sub);
        Task InsertToken(TokenEntity entity);
        void UpdateToken(TokenEntity entity);
        void DeleteToken(TokenEntity entity);
        Task Save();
    }

}
