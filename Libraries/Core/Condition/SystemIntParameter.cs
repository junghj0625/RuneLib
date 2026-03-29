using System;



namespace Rune
{
    public class ConditionSystemIntParameter : Condition
    {
        public ConditionSystemIntParameter()
        {
            
        }

        public ConditionSystemIntParameter(ConditionSystemIntParameterData data)
        {
            ParameterName = data.parameterName;

            CompareValue = data.compareValue;
            
            ComparisonType = data.comparisonType;
        }



        public override bool Result
        {
            get
            {
                if (!ParameterManager.SystemParameters.TryGetInt(ParameterName, out var value)) return false;

                return ComparisonType switch
                {
                    ConditionIntComparisonType.Equal => value == CompareValue,
                    ConditionIntComparisonType.GreaterOrEqual => value >= CompareValue,
                    ConditionIntComparisonType.LessOrEqual => value <= CompareValue,
                    _ => false,
                };
            }
        }



        public string ParameterName { get; set; } = string.Empty;

        public int CompareValue { get; set; } = 0;

        public ConditionIntComparisonType ComparisonType { get; set; }
    }



    [Serializable]
    [Condition(typeof(ConditionSystemIntParameter))]
    public class ConditionSystemIntParameterData : ConditionData
    {
        public string parameterName;

        public int compareValue;
        
        public ConditionIntComparisonType comparisonType;
    }
}
