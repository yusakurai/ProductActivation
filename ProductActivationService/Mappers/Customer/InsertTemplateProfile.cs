using AutoMapper;
using ProductActivationService.Models;
using ProductActivationService.Entities;

namespace ProductActivationService.Mappers
{
  public class InsertCustomerProfile : Profile
  {
    public InsertCustomerProfile()
    {
      CreateMap<InsertCustomerModel, CustomerEntity>();
    }
  }
}
