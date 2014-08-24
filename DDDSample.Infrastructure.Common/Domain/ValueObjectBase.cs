using System.Collections.Generic;
using System.Text;

namespace DDDSample.Infrastructure.Common.Domain
{
    public abstract class ValueObjectBase
    {
        private List<BusinessRule> _brokenRules;

        public ValueObjectBase()
        {
        }

        protected abstract void Validate();

        public void ThrowIfInvalid()
        {
            _brokenRules.Clear();
            Validate();
            if (_brokenRules.Count() > 0)
            {
                StringBuilder issues = new StringBuilder();
                foreach (var businessRule in _brokenRules)
                {
                    issues.AppendLine(businessRule.RuleDescription);
                }
                throw new ValueObjectIsInvalidException(issues.ToString());
            }
        }

        protected void AddBrokenRule(BusinessRule rule)
        {
            _brokenRules.Add(rule);
        }
    }
}