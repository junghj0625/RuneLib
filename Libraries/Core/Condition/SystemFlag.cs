using System;



namespace Rune
{
    public class ConditionSystemFlag : Condition
    {
        public ConditionSystemFlag()
        {
            
        }

        public ConditionSystemFlag(ConditionSystemFlagData data)
        {
            Flag = data.flag;

            ComparisonType = data.comparisonType;
        }



        public override bool Result
        {
            get => ComparisonType switch
            {
                ConditionFlagComparisonType.Exist => ParameterManager.SystemParameters.HasFlag(Flag),
                ConditionFlagComparisonType.NotExist => !ParameterManager.SystemParameters.HasFlag(Flag),
                _ => false,
            };
        }



        public string Flag { get; set; } = string.Empty;

        public ConditionFlagComparisonType ComparisonType { get; set; }
    }



    [Serializable]
    [Condition(typeof(ConditionSystemFlag))]
    public class ConditionSystemFlagData : ConditionData
    {
        public string flag;

        public ConditionFlagComparisonType comparisonType;
    }
}