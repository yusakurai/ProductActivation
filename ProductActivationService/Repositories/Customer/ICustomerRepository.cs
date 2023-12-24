using ProductActivationService.Entities;

namespace ProductActivationService.Repositories
{
    /// <summary>
    /// 顧客リポジトリー インターフェイス
    /// </summary>
    public interface ICustomerRepository
    {
        ValueTask<IEnumerable<CustomerEntity>> GetCustomers(string? key = null);
        ValueTask<CustomerEntity?> GetCustomerByID(long id);
        Task InsertCustomer(CustomerEntity entity);
        void UpdateCustomer(CustomerEntity entity);
        void DeleteCustomer(CustomerEntity entity);
        Task Save();
    }

}
