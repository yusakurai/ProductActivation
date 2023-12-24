using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;
using ProductActivationService.Entities;

namespace ProductActivationService.Repositories
{
    /// <summary>
    /// トークン リポジトリー
    /// </summary>
    public class TokenRepository(MainContext context, ILogger<ITokenRepository> logger) : ITokenRepository
    {
        private ILogger Logger => logger;
        private MainContext Context => context;

        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="sub">sub</param>
        public async ValueTask<IEnumerable<TokenEntity>> GetList(string? sub = null)
        {
            var query = Context.Token.Where(x => x.DeletedAt == null);
            if (!string.IsNullOrEmpty(sub))
            {
                query = query.Where(x => x.Sub.ToString().Contains(sub));
            }
            var entityList = await query.ToListAsync();
            return entityList;
        }

        /// <summary>
        /// 詳細取得
        /// </summary>
        /// <param name="sub">sub</param>
        public async ValueTask<TokenEntity?> GetDetail(string sub)
        {
            return await Context.Token.SingleOrDefaultAsync(e => e.Sub.ToString() == sub);
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity">エンティティ</param>
        public async Task Insert(TokenEntity entity)
        {
            await Context.Token.AddAsync(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">エンティティ</param>
        public void Update(TokenEntity entity)
        {
            Context.Token.Update(entity);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="entity">エンティティ</param>
        public void Delete(TokenEntity entity)
        {
            entity.DeletedAt = DateTime.Now;
            Context.Token.Update(entity);
        }

        /// <summary>
        /// DB保存
        /// </summary>
        public async Task Save()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "context.SaveChangesAsyncで例外発生");
                throw;
            }
        }
    }

}
