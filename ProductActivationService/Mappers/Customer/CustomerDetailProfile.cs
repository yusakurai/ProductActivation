using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;

namespace ProductActivationService.Mappers
{
    /// <summary>
    /// 顧客詳細マッパー
    /// </summary>
    public class CustomerDetailProfile : Profile
    {
        public CustomerDetailProfile()
        {
            CreateMap<CustomerEntity, CustomerDetailModel>()
                .ForMember(dst => dst.CreatedAt, src => src.MapFrom(s => s.CreatedAt))
                .ForMember(dst => dst.UpdatedAt, src => src.MapFrom(s => s.UpdatedAt))
                .ForMember(dst => dst.DeletedAt, src => src.MapFrom(s => s.DeletedAt))
                .ForMember(dst => dst.Id, src => src.MapFrom(s => s.Id))
                .ForMember(dst => dst.Name, src => src.MapFrom(s => s.Name))
                .ForMember(dst => dst.ProductKey, src => src.MapFrom(s => s.ProductKey))
                .ForMember(dst => dst.LicenseLimit, src => src.MapFrom(s => s.LicenseLimit));
        }
    }
}
