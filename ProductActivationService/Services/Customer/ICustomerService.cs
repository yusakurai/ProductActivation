using ProductActivationService.Model;

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

    ValueTask<List<CustomerModel>> GetCustomers(string? key);
    ValueTask<CustomerModel?> GetCustomer(long id);
    ValueTask<(CustomerModel?, ServiceStatus)> InsertCustomer(InsertCustomerModel model);
    ValueTask<(CustomerModel?, ServiceStatus)> UpdateCustomer(long id, UpdateCustomerModel model);
    ValueTask<ServiceStatus> DeleteCustomer(long id);
  }

}