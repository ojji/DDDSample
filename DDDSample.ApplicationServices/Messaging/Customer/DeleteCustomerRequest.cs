namespace DDDSample.ApplicationServices.Messaging.Customer
{
    public class DeleteCustomerRequest : IntegerIdRequest
    {
        public DeleteCustomerRequest(int customerId) : base(customerId)
        {
        }
    }
}