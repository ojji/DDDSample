namespace DDDSample.ApplicationServices.Messaging.Customer
{
    public class GetCustomerRequest : IntegerIdRequest
    {
        public GetCustomerRequest(int customerId) : base(customerId) { }
    }
}