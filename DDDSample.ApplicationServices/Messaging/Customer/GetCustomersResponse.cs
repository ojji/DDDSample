using System.Collections.Generic;
using DDDSample.ApplicationServices.ViewModels;

namespace DDDSample.ApplicationServices.Messaging.Customer
{
    public class GetCustomersResponse : ServiceResponseBase
    {
        public IEnumerable<CustomerViewModel> Customers { get; set; }
    }
}