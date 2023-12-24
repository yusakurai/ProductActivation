using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;

namespace ProductActivationService.Mappers
{
    /// <summary>
    /// 顧客一覧マッパー
    /// </summary>
    public class CustomerListProfile : Profile
    {
        public CustomerListProfile()
        {
            CreateMap<CustomerEntity, CustomerListModel>()
                .ForMember(dst => dst.Id, src => src.MapFrom(s => s.Id))
                .ForMember(dst => dst.Name, src => src.MapFrom(s => s.Name))
                .ForMember(dst => dst.ProductKey, src => src.MapFrom(s => s.ProductKey))
                .ForMember(dst => dst.LicenseLimit, src => src.MapFrom(s => s.LicenseLimit));
        }
    }
}
