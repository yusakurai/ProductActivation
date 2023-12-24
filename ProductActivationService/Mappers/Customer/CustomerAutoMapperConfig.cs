using AutoMapper;

namespace ProductActivationService.Mappers
{
    /// <summary>
    /// 顧客マッパー設定
    /// </summary>
    public class CustomerAutoMapperConfig
    {
        public static void AddProfile(IMapperConfigurationExpression config)
        {
            config.AddProfile<CustomerListProfile>();
            config.AddProfile<CustomerDetailProfile>();
            config.AddProfile<CustomerUpdateProfile>();
        }
    }
}
