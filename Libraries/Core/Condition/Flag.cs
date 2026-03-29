using System;



namespace Rune
{
    public class ConditionFlag : Condition
    {
        public ConditionFlag()
        {
            
        }

        public ConditionFlag(ConditionFlagData data)
        {
            Flag = data.flag;

            ComparisonType = data.comparisonType;
        }



        public override bool Result
        {
            get => ComparisonType switch
            {
                ConditionFlagComparisonType.Exist => ParameterManager.Parameters.HasFlag(Flag),
                ConditionFlagComparisonType.NotExist => !ParameterManager.Parameters.HasFlag(Flag),
                _ => false,
            };
        }



        public string Flag { get; set; } = string.Empty;

        public ConditionFlagComparisonType ComparisonType { get; set; }
    }



    [Serializable]
    [Condition(typeof(ConditionFlag))]
    public class ConditionFlagData : ConditionData
    {
        public string flag;

        public ConditionFlagComparisonType comparisonType;
    }



    public enum ConditionFlagComparisonType
    {
        Exist,
        NotExist,
    }
}