using System;



namespace Rune
{
    public class GameEventAddFlag : GameEventAction
    {
        public GameEventAddFlag() : base()
        {
            
        }

        public GameEventAddFlag(GameEventAddFlagData data) : base(data)
        {
            Flag = data.flag;
        }


        
        public override void Play()
        {
            ParameterManager.Parameters.AddFlag(Flag);
        }



        public string Flag { get; set; } = null;
    }



    [Serializable]
    [GameEvent(typeof(GameEventAddFlag))]
    public class GameEventAddFlagData : GameEventData
    {
        public string flag;
    }
}