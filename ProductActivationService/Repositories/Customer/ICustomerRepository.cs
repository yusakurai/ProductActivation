using ProductActivationService.Entities;

namespace ProductActivationService.Repositories
{
    /// <summary>
    /// 顧客リポジトリー インターフェイス
    /// </summary>
    public interface ICustomerRepository
    {
        ValueTask<IEnumerable<CustomerEntity>> GetList(string? name = null);
        ValueTask<CustomerEntity?> GetDetail(long id);
        Task Insert(CustomerEntity entity);
        void Update(CustomerEntity entity);
        void Delete(CustomerEntity entity);
        Task Save();
    }

}
