using System;



namespace Rune
{
    public class GameEventRemoveFlag : GameEventAction
    {
        public GameEventRemoveFlag() : base()
        {
            
        }

        public GameEventRemoveFlag(GameEventRemoveFlagData data) : base(data)
        {
            Flag = data.flag;
        }


        
        public override void Play()
        {
            ParameterManager.Parameters.RemoveFlag(Flag);
        }



        public string Flag { get; set; } = null;
    }



    [Serializable]
    [GameEvent(typeof(GameEventRemoveFlag))]
    public class GameEventRemoveFlagData : GameEventData
    {
        public string flag;
    }
}