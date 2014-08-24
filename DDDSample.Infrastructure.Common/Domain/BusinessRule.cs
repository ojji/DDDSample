namespace DDDSample.Infrastructure.Common.Domain
{
    public class BusinessRule
    {
        private readonly string _ruleDescription;

        public BusinessRule(string ruleDescription)
        {
            _ruleDescription = ruleDescription;
        }

        public string RuleDescription
        {
            get { return _ruleDescription; }
        }
    }
}