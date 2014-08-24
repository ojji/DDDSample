using System;
using DDDSample.Infrastructure.Common.Domain;
using DDDSample.Infrastructure.Common.UnitOfWork;
using DDDSample.Repository.Memory.Database;

namespace DDDSample.Repository.Memory
{
    public abstract class Repository<DomainType, IdType, DatabaseType>
        : IUnitOfWorkRepository where DomainType
        : IAggregateRoot
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObjectContextFactory _objectContextFactory;

        public IObjectContextFactory ObjectContextFactory
        {
            get { return _objectContextFactory; }
        }

        public Repository(IUnitOfWork unitOfWork, IObjectContextFactory objectContextFactory)
        {
            if (unitOfWork == null) { throw new ArgumentNullException("unitOfWork"); }
            if (objectContextFactory == null) { throw new ArgumentNullException("objectContextFactory"); }
            _unitOfWork = unitOfWork;
            _objectContextFactory = objectContextFactory;
        }

        public void PersistInsertion(IAggregateRoot aggregateRoot)
        {
            DatabaseType databaseType = RetrieveDatabaseTypeFrom(aggregateRoot);
            _objectContextFactory.Create().AddEntity<DatabaseType>(databaseType);
        }

        public void PersistUpdate(IAggregateRoot aggregateRoot)
        {
            DatabaseType databaseType = RetrieveDatabaseTypeFrom(aggregateRoot);
            _objectContextFactory.Create().UpdateEntity<DatabaseType>(databaseType);
        }

        public void PersistDeletion(IAggregateRoot aggregateRoot)
        {
            DatabaseType databaseType = RetrieveDatabaseTypeFrom(aggregateRoot);
            _objectContextFactory.Create().DeleteEntity<DatabaseType>(databaseType);
        }

        public abstract DatabaseType ConvertToDatabaseType(DomainType domainType);

        public abstract DomainType FindBy(IdType id);

        public void Update(DomainType aggregateRoot)
        {
            _unitOfWork.RegisterUpdate(aggregateRoot, this);
        }

        public void Insert(DomainType aggregateRoot)
        {
            _unitOfWork.RegisterInsertion(aggregateRoot, this);
        }

        public void Delete(DomainType aggregateRoot)
        {
            _unitOfWork.RegisterDeletion(aggregateRoot, this);
        }
        
        private DatabaseType RetrieveDatabaseTypeFrom(IAggregateRoot aggregateRoot)
        {
            var domainType = (DomainType)aggregateRoot;
            var databaseType = ConvertToDatabaseType(domainType);
            return databaseType;
        }
    }
}