using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;
using ProductActivationService.Entities;

namespace ProductActivationService.Repositories
{
    /// <summary>
    /// Customerデータ取得リポジトリ
    /// </summary>
    public class CustomerRepositoryFake() : ICustomerRepository
    {

        /// <summary>
        /// Customerデータ取得（一覧）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async ValueTask<IEnumerable<CustomerEntity>> GetCustomers(string? name = null)
        {
            await Task.Delay(0);
            var customers = Enumerable.Range(1, 5).Select(index =>
            {
                var customer = new CustomerEntity()
                {
                    Id = 1,
                    Name = "hoge"
                };
                return customer;
            })
                .ToArray();
            return customers;
        }

        /// <summary>
        /// Customerデータ取得（主キー）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<CustomerEntity?> GetCustomerByID(long id)
        {
            await Task.Delay(0);
            var customer = new CustomerEntity()
            {
                Id = 1,
                Name = "hoge"
            };
            return customer;
        }

        /// <summary>
        /// Customerデータ登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertCustomer(CustomerEntity entity)
        {
            await Task.Delay(0);
        }

        /// <summary>
        /// Customerデータ更新
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCustomer(CustomerEntity entity)
        {

        }

        /// <summary>
        /// Customerデータ削除
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteCustomer(CustomerEntity entity)
        {

        }

        /// <summary>
        /// DB保存
        /// </summary>
        public async Task Save()
        {
            await Task.Delay(0);
        }
    }

}
