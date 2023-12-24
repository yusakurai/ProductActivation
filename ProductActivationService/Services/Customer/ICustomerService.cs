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
        ValueTask<CustomerServiceResult> Insert(CustomerUpdateModel model);
        ValueTask<CustomerServiceResult> Update(long id, CustomerUpdateModel model);
        ValueTask<ServiceStatus> Delete(long id);
    }

    /// <summary>
    /// 顧客サービス 結果クラス
    /// </summary>
    public class CustomerServiceResult(CustomerDetailModel? detailModel, ICustomerService.ServiceStatus status)
    {
        public CustomerDetailModel? DetailModel => detailModel;
        public ICustomerService.ServiceStatus Status => status;
    }
}
