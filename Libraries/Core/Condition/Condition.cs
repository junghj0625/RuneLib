using System;



namespace Rune
{
    [Serializable]
    public abstract class Condition
    {
        public abstract bool Result { get; }
    }



    [Serializable]
    public abstract class ConditionData : IConditionData
    {
        
    }
}