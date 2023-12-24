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

        ValueTask<List<CustomerListModel>> GetList(string? name);
        ValueTask<CustomerDetailModel?> GetDetail(long id);
        ValueTask<(CustomerDetailModel?, ServiceStatus)> Insert(CustomerUpdateModel model);
        ValueTask<(CustomerDetailModel?, ServiceStatus)> Update(long id, CustomerUpdateModel model);
        ValueTask<ServiceStatus> Delete(long id);
    }

}
