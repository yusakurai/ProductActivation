using AutoMapper;
using ProductActivationService.Models;
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
