using AutoMapper;
using ProductActivationService.Models;
using ProductActivationService.Entities;

namespace ProductActivationService.Mapper
{
  public class CustomerProfile : Profile
  {
    public CustomerProfile()
    {
      CreateMap<CustomerEntity, CustomerModel>()
          .ForMember(dst => dst.Id, src => src.MapFrom(s => s.Id))
          .ForMember(dst => dst.Name, src => src.MapFrom(s => s.Name));
    }
  }
}
