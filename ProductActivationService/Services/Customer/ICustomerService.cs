using ProductActivationService.Models;

namespace ProductActivationService.Services
{
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
        ValueTask<(CustomerDetailModel?, ServiceStatus)> InsertCustomer(CustomerInsertModel model);
        ValueTask<(CustomerDetailModel?, ServiceStatus)> UpdateCustomer(long id, CustomerUpdateModel model);
        ValueTask<ServiceStatus> DeleteCustomer(long id);
    }

}
