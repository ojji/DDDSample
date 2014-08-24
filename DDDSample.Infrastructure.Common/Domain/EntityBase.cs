using System.Collections.Generic;

namespace DDDSample.Infrastructure.Common.Domain
{
    public abstract class EntityBase<IdType>
    {
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();

        public IdType Id { get; set; }

        public override bool Equals(object entity)
        {
            return entity != null && entity is EntityBase<IdType> && this == (EntityBase<IdType>) entity;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityBase<IdType> entity1, EntityBase<IdType> entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            if (entity1.Id.ToString() == entity2.Id.ToString())
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(EntityBase<IdType> entity1, EntityBase<IdType> entity2)
        {
            return !(entity1 == entity2);
        }

        protected abstract void Validate();

        protected void AddBrokenRule(BusinessRule rule)
        {
            _brokenRules.Add(rule);
        }

        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }
    }
}