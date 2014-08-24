using DDDSample.Infrastructure.Common.Domain;

namespace DDDSample.Domain.Customer
{
    public static class CustomerBusinessRules
    {
         public static readonly BusinessRule CustomerNameRequired = new BusinessRule("A customer must have a name.");
    }
}