using DDDSample.ApplicationServices.ViewModels;

namespace DDDSample.ApplicationServices.Messaging.Customer
{
    public class UpdateCustomerRequest : IntegerIdRequest
    {
        public UpdateCustomerRequest(int id) : base(id)
        {
        }

        public CustomerPropertiesViewModel CustomerProperties { get; set; }
    }
}