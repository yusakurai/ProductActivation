using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;

namespace ProductActivationService.Mappers
{
    public class CustomerUpdateProfile : Profile
    {
        public CustomerUpdateProfile()
        {
            CreateMap<CustomerUpdateModel, CustomerEntity>();
        }
    }
}
