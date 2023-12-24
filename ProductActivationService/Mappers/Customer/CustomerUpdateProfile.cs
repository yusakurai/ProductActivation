using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;

namespace ProductActivationService.Mappers
{
    /// <summary>
    /// 顧客更新マッパー
    /// </summary>
    public class CustomerUpdateProfile : Profile
    {
        public CustomerUpdateProfile()
        {
            CreateMap<CustomerUpdateModel, CustomerEntity>();
        }
    }
}
