using AutoMapper;

namespace ProductActivationService.Mappers
{
    public class AutoMapperConfig
    {
        public static void AddProfile(IMapperConfigurationExpression config)
        {
            config.AddProfile<CustomerProfile>();
            config.AddProfile<InsertCustomerProfile>();
            config.AddProfile<UpdateCustomerProfile>();
        }
    }
}
