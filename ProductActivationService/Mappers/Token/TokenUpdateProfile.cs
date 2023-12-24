using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;

namespace ProductActivationService.Mappers
{
    /// <summary>
    /// トークン更新マッパー
    /// </summary>
    public class TokenUpdateProfile : Profile
    {
        public TokenUpdateProfile()
        {
            CreateMap<TokenUpdateModel, TokenEntity>();
        }
    }
}
