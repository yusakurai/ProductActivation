using ProductActivationService.Models;

namespace ProductActivationService.Services
{
    /// <summary>
    /// 顧客サービス インターフェイス
    /// </summary>
    public interface ICustomerService
    {
        enum ServiceStatus
        {
            Ok,
            NotFound,
            Conflict,
        }

        ValueTask<List<CustomerListModel>> GetCustomers(string? key);
        ValueTask<CustomerDetailModel?> GetCustomer(long id);
        ValueTask<(CustomerDetailModel?, ServiceStatus)> InsertCustomer(CustomerUpdateModel model);
        ValueTask<(CustomerDetailModel?, ServiceStatus)> UpdateCustomer(long id, CustomerUpdateModel model);
        ValueTask<ServiceStatus> DeleteCustomer(long id);
    }

}
