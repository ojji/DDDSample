using System.Collections.Generic;
using System.Linq;
using DDDSample.ApplicationServices.ViewModels;
using DDDSample.Domain.Customer;

namespace DDDSample
{
    public static class ConversionHelper
    {
        public static CustomerViewModel ConvertToViewModel(this Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                AddressLine1 = customer.CustomerAddress.AddressLine1,
                AddressLine2 = customer.CustomerAddress.AddressLine2,
                City = customer.CustomerAddress.City,
                PostalCode = customer.CustomerAddress.PostalCode
            };
        }

        public static IEnumerable<CustomerViewModel> ConvertToViewModels(this IEnumerable<Customer> customers)
        {
            return customers.Select(customer => customer.ConvertToViewModel()).ToList();
        }
    }
}