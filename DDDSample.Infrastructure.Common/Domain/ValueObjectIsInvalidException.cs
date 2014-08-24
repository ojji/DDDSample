using System;

namespace DDDSample.Infrastructure.Common.Domain
{
    public class ValueObjectIsInvalidException : Exception
    {
        public ValueObjectIsInvalidException(string message) : base(message) { }
    }
}
