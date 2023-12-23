using AutoMapper;
using ProductActivationService.Model;
using ProductActivationService.Entity;

namespace ProductActivationService.Mapper
{
  public class InsertCustomerProfile : Profile
  {
    public InsertCustomerProfile()
    {
      CreateMap<InsertCustomerModel, CustomerEntity>();
    }
  }
}
