using AutoMapper;

namespace ProductActivationService.Mappers
{
    public class CustomerAutoMapperConfig
    {
        public static void AddProfile(IMapperConfigurationExpression config)
        {
            config.AddProfile<CustomerListProfile>();
            config.AddProfile<CustomerDetailProfile>();
            config.AddProfile<CustomerInsertProfile>();
            config.AddProfile<CustomerUpdateProfile>();
        }
    }
}
