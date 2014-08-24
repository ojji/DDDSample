using DDDSample.ApplicationServices.ViewModels;

namespace DDDSample.ApplicationServices.Messaging.Customer
{
    public class InsertCustomerRequest : ServiceRequestBase
    {
        public CustomerPropertiesViewModel CustomerProperties { get; set; }
    }
}