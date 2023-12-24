using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;
using ProductActivationService.Entities;

namespace ProductActivationService.Repositories
{
    /// <summary>
    /// 顧客 リポジトリー
    /// </summary>
    public class CustomerRepository(MainContext context, ILogger<ICustomerRepository> logger) : ICustomerRepository
    {
        private ILogger Logger => logger;
        private MainContext Context => context;

        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="name">顧客名</param>
        public async ValueTask<IEnumerable<CustomerEntity>> GetList(string? name = null)
        {
            var query = Context.Customer.Where(x => x.DeletedAt == null);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name != null && x.Name.Contains(name));
            }
            var entityList = await query.ToListAsync();
            return entityList;
        }

        /// <summary>
        /// 詳細取得
        /// </summary>
        /// <param name="id">ID</param>
        public async ValueTask<CustomerEntity?> GetDetail(long id)
        {
            return await Context.Customer.SingleOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity">エンティティ</param>
        public async Task Insert(CustomerEntity entity)
        {
            await Context.Customer.AddAsync(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">エンティティ</param>
        public void Update(CustomerEntity entity)
        {
            Context.Customer.Update(entity);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="entity">エンティティ</param>
        public void Delete(CustomerEntity entity)
        {
            entity.DeletedAt = DateTime.Now;
            Context.Customer.Update(entity);
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
