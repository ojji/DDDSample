using DDDSample.Infrastructure.Common.Domain;

namespace DDDSample.Domain.ValueObjects
{
    public static class AddressBusinessRules
    {
        public static readonly BusinessRule CityInAddressRequired = new BusinessRule("An address must have a city.");
    }
}