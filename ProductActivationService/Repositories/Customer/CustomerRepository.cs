using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;
using ProductActivationService.Entities;

namespace ProductActivationService.Repositories
{
  /// <summary>
  /// Customerデータ取得リポジトリ
  /// </summary>
  public class CustomerRepository(MainContext context, ILogger<ICustomerRepository> logger) : ICustomerRepository
  {
    private ILogger Logger => logger;
    private MainContext Context => context;

    /// <summary>
    /// Customerデータ取得（一覧）
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async ValueTask<IEnumerable<CustomerEntity>> GetCustomers(string? name = null)
    {
      var query = Context.Customer.Where(e => true);
      if (!string.IsNullOrEmpty(name))
      {
        query = query.Where(x => x.Name == name);
      }
      var result = await query.ToListAsync();
      return result;
    }

    /// <summary>
    /// Customerデータ取得（主キー）
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async ValueTask<CustomerEntity?> GetCustomerByID(long id)
    {
      return await Context.Customer.SingleOrDefaultAsync(e => e.Id == id);
    }

    /// <summary>
    /// Customerデータ登録
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task InsertCustomer(CustomerEntity entity)
    {
      await Context.Customer.AddAsync(entity);
    }

    /// <summary>
    /// Customerデータ更新
    /// </summary>
    /// <param name="entity"></param>
    public void UpdateCustomer(CustomerEntity entity)
    {
      Context.Customer.Update(entity);
    }

    /// <summary>
    /// Customerデータ削除
    /// </summary>
    /// <param name="entity"></param>
    public void DeleteCustomer(CustomerEntity entity)
    {
      Context.Customer.Remove(entity);
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