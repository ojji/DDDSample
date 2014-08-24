using DDDSample.ApplicationServices.ViewModels;

namespace DDDSample.ApplicationServices.Messaging.Customer
{
    public class GetCustomerResponse : ServiceResponseBase
    {
        public CustomerViewModel Customer { get; set; }
    }
}