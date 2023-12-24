using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;

namespace ProductActivationService.Mappers
{
    /// <summary>
    /// トークン詳細マッパー
    /// </summary>
    public class TokenDetailProfile : Profile
    {
        public TokenDetailProfile()
        {
            CreateMap<TokenEntity, TokenDetailModel>()
                .ForMember(dst => dst.CreatedAt, src => src.MapFrom(s => s.CreatedAt))
                .ForMember(dst => dst.UpdatedAt, src => src.MapFrom(s => s.UpdatedAt))
                .ForMember(dst => dst.DeletedAt, src => src.MapFrom(s => s.DeletedAt))
                .ForMember(dst => dst.Sub, src => src.MapFrom(s => s.Sub))
                .ForMember(dst => dst.CustomerId, src => src.MapFrom(s => s.CustomerId))
                .ForMember(dst => dst.ClientGuid, src => src.MapFrom(s => s.ClientGuid))
                .ForMember(dst => dst.ClientHostName, src => src.MapFrom(s => s.ClientHostName))
                .ForMember(dst => dst.Exp, src => src.MapFrom(s => s.Exp));
        }
    }
}
