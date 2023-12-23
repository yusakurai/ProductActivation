using AutoMapper;

namespace ProductActivationService.Mapper
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
