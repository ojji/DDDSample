namespace DDDSample.Infrastructure.Common.Domain
{
    public interface IRepository<AggregateType, IdType> 
        : IReadOnlyRepository<AggregateType, IdType> where AggregateType 
            : IAggregateRoot
    {
        void Insert(AggregateType aggregate);
        void Update(AggregateType aggregate);
        void Delete(AggregateType aggregate);
    }
}