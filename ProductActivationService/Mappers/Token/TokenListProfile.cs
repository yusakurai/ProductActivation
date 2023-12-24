using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;

namespace ProductActivationService.Mappers
{
    /// <summary>
    /// トークン一覧マッパー
    /// </summary>
    public class TokenListProfile : Profile
    {
        public TokenListProfile()
        {
            CreateMap<TokenEntity, TokenListModel>()
                .ForMember(dst => dst.Sub, src => src.MapFrom(s => s.Sub))
                .ForMember(dst => dst.CustomerId, src => src.MapFrom(s => s.CustomerId))
                .ForMember(dst => dst.ClientGuid, src => src.MapFrom(s => s.ClientGuid))
                .ForMember(dst => dst.ClientHostName, src => src.MapFrom(s => s.ClientHostName))
                .ForMember(dst => dst.Exp, src => src.MapFrom(s => s.Exp));
        }
    }
}
