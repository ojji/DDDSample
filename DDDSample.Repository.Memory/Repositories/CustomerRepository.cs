using System;
using System.Collections.Generic;
using System.Linq;
using DDDSample.Domain.Customer;
using DDDSample.Domain.ValueObjects;
using DDDSample.Infrastructure.Common.UnitOfWork;
using DDDSample.Repository.Memory.Database;

namespace DDDSample.Repository.Memory.Repositories
{
    public class CustomerRepository : Repository<Customer, int, DatabaseCustomer>, ICustomerRepository
    {

        #region ICustomerRepository Members
        public CustomerRepository(IUnitOfWork unitOfWork, IObjectContextFactory objectContextFactory) : base(unitOfWork, objectContextFactory) { }

        public IEnumerable<Customer> FindAll()
        {
            List<Customer> allCustomers = new List<Customer>();
            List<DatabaseCustomer> databaseCustomers =
                (from databaseCustomer in ObjectContextFactory.Create().DatabaseCustomers select databaseCustomer
                    ).ToList();

            foreach (var databaseCustomer in databaseCustomers)
            {
                allCustomers.Add(ConvertToDomainType(databaseCustomer));
            }
            return allCustomers;
        }
        #endregion
        
        public override DatabaseCustomer ConvertToDatabaseType(Customer domainType)
        {
            var databaseType = new DatabaseCustomer()
            {
                Id = domainType.Id,
                Name = domainType.Name,
                Address = domainType.CustomerAddress.AddressLine1,
                City = domainType.CustomerAddress.City,
                Country = "N/A",
                Telephone = "N/A"
            };

            return databaseType;
        }

        public override Customer FindBy(int id)
        {
            var dbCustomer = (from databaseCustomer in ObjectContextFactory.Create().DatabaseCustomers
                              where databaseCustomer.Id == id
                              select databaseCustomer).FirstOrDefault();

            if (dbCustomer != null)
            {
                return ConvertToDomainType(dbCustomer);
            }

            return null;
        }

        private Customer ConvertToDomainType(DatabaseCustomer dbCustomer)
        {
            var customer = new Customer
            {
                Id = dbCustomer.Id,
                Name = dbCustomer.Name,
                CustomerAddress = new Address
                {
                    AddressLine1 = dbCustomer.Address,
                    AddressLine2 = string.Empty,
                    City = dbCustomer.City,
                    PostalCode = "N/A"
                }
            };
            return customer;
        }
    }
}