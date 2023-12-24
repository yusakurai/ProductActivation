using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;

namespace ProductActivationService.Mappers
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
