using DDDSample.Infrastructure.Common.Domain;

namespace DDDSample.Domain.ValueObjects
{
    public class Address : ValueObjectBase
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(City))
            {
                AddBrokenRule(AddressBusinessRules.CityInAddressRequired);
            }
        }
    }
}