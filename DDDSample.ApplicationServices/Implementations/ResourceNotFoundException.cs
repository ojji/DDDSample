using System;

namespace DDDSample.ApplicationServices.Implementations
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() : this("The requested resource was not found.") { }
        public ResourceNotFoundException(string message) : base(message) { }
    }
}