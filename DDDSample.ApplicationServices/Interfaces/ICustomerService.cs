using DDDSample.ApplicationServices.Messaging.Customer;

namespace DDDSample.ApplicationServices.Interfaces
{
    public interface ICustomerService
    {
        GetCustomerResponse GetCustomer(GetCustomerRequest getCustomerRequest);
        GetCustomersResponse GetAllCustomers();
        InsertCustomerResponse InsertCustomer(InsertCustomerRequest insertCustomerRequest);
        UpdateCustomerResponse UpdateCustomer(UpdateCustomerRequest updateCustomerRequest);
        DeleteCustomerResponse DeleteCustomer(DeleteCustomerRequest deleteCustomerRequest);
    }
}