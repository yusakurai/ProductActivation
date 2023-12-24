using ProductActivationService.Entities;

namespace ProductActivationService.Repositories
{
    /// <summary>
    /// トークンリポジトリー インターフェイス
    /// </summary>
    public interface ITokenRepository
    {
        ValueTask<IEnumerable<TokenEntity>> GetList(string? sub = null);
        ValueTask<TokenEntity?> GetDetail(string sub);
        Task Insert(TokenEntity entity);
        void Update(TokenEntity entity);
        void Delete(TokenEntity entity);
        Task Save();
    }

}
