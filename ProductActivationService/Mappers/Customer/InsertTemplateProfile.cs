using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;

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
