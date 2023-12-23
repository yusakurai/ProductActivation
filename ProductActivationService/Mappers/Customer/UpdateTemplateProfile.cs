using AutoMapper;
using ProductActivationService.Models;
using ProductActivationService.Entities;

namespace ProductActivationService.Mappers
{
  public class UpdateCustomerProfile : Profile
  {
    public UpdateCustomerProfile()
    {
      CreateMap<UpdateCustomerModel, CustomerEntity>();
    }
  }
}
