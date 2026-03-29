using System;



namespace Rune
{
    public class ConditionIntParameter : Condition
    {
        public ConditionIntParameter()
        {
            
        }

        public ConditionIntParameter(ConditionIntParameterData data)
        {
            ParameterName = data.parameterName;

            CompareValue = data.compareValue;
            
            ComparisonType = data.comparisonType;
        }



        public override bool Result
        {
            get
            {
                if (!ParameterManager.Parameters.TryGetInt(ParameterName, out var value)) return false;

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
    [Condition(typeof(ConditionIntParameter))]
    public class ConditionIntParameterData : ConditionData
    {
        public string parameterName;

        public int compareValue;
        
        public ConditionIntComparisonType comparisonType;
    }



    public enum ConditionIntComparisonType
    {
        Equal,
        GreaterOrEqual,
        LessOrEqual
    }
}
