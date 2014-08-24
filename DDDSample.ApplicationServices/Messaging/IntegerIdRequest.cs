using System;

namespace DDDSample.ApplicationServices.Messaging
{
    public class IntegerIdRequest : ServiceRequestBase
    {
        private readonly int _id;

        public IntegerIdRequest(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Id must be positive.");
            }

            _id = id;
        }

        public int Id { get { return _id; } }
    }
}