using System;



namespace Rune
{
    public class GameEventAddIntParameter : GameEventAction
    {
        public GameEventAddIntParameter() : base()
        {
            
        }

        public GameEventAddIntParameter(GameEventAddIntParameterData data) : base(data)
        {
            Key = data.key;

            Value = data.value;
        }



        public override void Play()
        {
            ParameterManager.Parameters.AddInt(Key, Value);
        }



        public string Key { get; set; } = string.Empty;

        public int Value { get; set; } = 0;
    }



    [Serializable]
    [GameEvent(typeof(GameEventAddIntParameter))]
    public class GameEventAddIntParameterData : GameEventData
    {
        public string key;

        public int value;
    }
}