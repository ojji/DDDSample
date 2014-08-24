using System.Collections.Generic;
using DDDSample.Infrastructure.Common.Domain;

namespace DDDSample.Domain.Customer
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        IEnumerable<Customer> FindAll();
    }
}