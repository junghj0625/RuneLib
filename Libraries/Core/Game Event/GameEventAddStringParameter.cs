using System;



namespace Rune
{
    public class GameEventAddStringParameter : GameEventAction
    {
        public GameEventAddStringParameter() : base()
        {
            
        }

        public GameEventAddStringParameter(GameEventAddStringParameterData data) : base(data)
        {
            Key = data.key;

            Value = data.value;
        }



        public override void Play()
        {
            ParameterManager.Parameters.AddString(Key, Value);
        }



        public string Key { get; set; }

        public string Value { get; set; }
    }



    [Serializable]
    [GameEvent(typeof(GameEventAddStringParameter))]
    public class GameEventAddStringParameterData : GameEventData
    {
        public string key;

        public string value;
    }
}