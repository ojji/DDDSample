using DDDSample.Domain.ValueObjects;
using DDDSample.Infrastructure.Common.Domain;

namespace DDDSample.Domain.Customer
{
    public class Customer : EntityBase<int>, IAggregateRoot
    {
        public string Name { get; set; }
        public Address CustomerAddress { get; set; }
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(CustomerBusinessRules.CustomerNameRequired);
            }
            CustomerAddress.ThrowIfInvalid();
        }
    }
}