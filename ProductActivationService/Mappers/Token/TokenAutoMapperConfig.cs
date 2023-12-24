using AutoMapper;

namespace ProductActivationService.Mappers
{
    /// <summary>
    /// トークンマッパー設定
    /// </summary>
    public class TokenAutoMapperConfig
    {
        public static void AddProfile(IMapperConfigurationExpression config)
        {
            config.AddProfile<TokenListProfile>();
            config.AddProfile<TokenDetailProfile>();
            config.AddProfile<TokenUpdateProfile>();
        }
    }
}
