using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;

namespace ProductActivationService.Mappers
{
    public class CustomerInsertProfile : Profile
    {
        public CustomerInsertProfile()
        {
            CreateMap<CustomerUpdateModel, CustomerEntity>();
        }
    }
}
