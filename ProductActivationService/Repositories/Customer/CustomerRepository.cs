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
        public async ValueTask<IEnumerable<CustomerEntity>> GetCustomers(string? name = null)
        {
            var query = Context.Customer.Where(x => x.DeletedAt == null);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name != null && x.Name.Contains(name));
            }
            var result = await query.ToListAsync();
            return result;
        }

        /// <summary>
        /// 詳細取得
        /// </summary>
        /// <param name="id">顧客ID</param>
        public async ValueTask<CustomerEntity?> GetCustomerByID(long id)
        {
            return await Context.Customer.SingleOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="entity">顧客エンティティ</param>
        public async Task InsertCustomer(CustomerEntity entity)
        {
            await Context.Customer.AddAsync(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">顧客エンティティ</param>
        public void UpdateCustomer(CustomerEntity entity)
        {
            Context.Customer.Update(entity);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="entity">顧客エンティティ</param>
        public void DeleteCustomer(CustomerEntity entity)
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
