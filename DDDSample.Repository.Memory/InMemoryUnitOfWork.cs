using System.Collections.Generic;
using System.Transactions;
using DDDSample.Infrastructure.Common.Domain;
using DDDSample.Infrastructure.Common.UnitOfWork;

namespace DDDSample.Repository.Memory
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        private Dictionary<IAggregateRoot, IUnitOfWorkRepository> _insertionDictionary;
        private Dictionary<IAggregateRoot, IUnitOfWorkRepository> _updateDictionary;
        private Dictionary<IAggregateRoot, IUnitOfWorkRepository> _deletionDictionary;

        public InMemoryUnitOfWork()
        {
            _insertionDictionary = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
            _updateDictionary = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
            _deletionDictionary = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
        }

        public void RegisterInsertion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        {
            if (!_insertionDictionary.ContainsKey(aggregateRoot))
            {
                _insertionDictionary.Add(aggregateRoot, repository);
            }
        }

        public void RegisterUpdate(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        {
            if (!_updateDictionary.ContainsKey(aggregateRoot))
            {
                _updateDictionary.Add(aggregateRoot, repository);
            }
        }

        public void RegisterDeletion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        {
            if (!_deletionDictionary.ContainsKey(aggregateRoot))
            {
                _deletionDictionary.Add(aggregateRoot, repository);
            }
        }

        public void Commit()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var aggregateRoot in _insertionDictionary.Keys)
                {
                    _insertionDictionary[aggregateRoot].PersistInsertion(aggregateRoot);
                }

                foreach (var aggregateRoot in _updateDictionary.Keys)
                {
                    _updateDictionary[aggregateRoot].PersistUpdate(aggregateRoot);
                }

                foreach (var aggregateRoot in _deletionDictionary.Keys)
                {
                    _deletionDictionary[aggregateRoot].PersistDeletion(aggregateRoot);
                }

                scope.Complete();
            }
        }
    }
}