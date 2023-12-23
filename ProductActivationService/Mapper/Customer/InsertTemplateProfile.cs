using AutoMapper;
using ProductActivationService.Models;
using ProductActivationService.Entities;

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
