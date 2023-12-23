using AutoMapper;
using ProductActivationService.Model;
using ProductActivationService.Entities;

namespace ProductActivationService.Mapper
{
  public class UpdateCustomerProfile : Profile
  {
    public UpdateCustomerProfile()
    {
      CreateMap<UpdateCustomerModel, CustomerEntity>();
    }
  }
}
