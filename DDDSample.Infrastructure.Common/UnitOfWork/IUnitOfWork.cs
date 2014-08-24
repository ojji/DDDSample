using DDDSample.Infrastructure.Common.Domain;

namespace DDDSample.Infrastructure.Common.UnitOfWork
{
    public interface IUnitOfWork
    {
        void RegisterInsertion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
        void RegisterUpdate(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
        void RegisterDeletion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
        void Commit();
    }
}