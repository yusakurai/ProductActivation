using Microsoft.AspNetCore.Mvc;

namespace ProductActivationService.Requests
{
    public class ListCustomerRequest
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
