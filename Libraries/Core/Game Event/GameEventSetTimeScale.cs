using System;



namespace Rune
{
    public class GameEventSetTimeScale : GameEventAction
    {
        public GameEventSetTimeScale() : base()
        {
            
        }

        public GameEventSetTimeScale(GameEventSetTimeScaleData data) : base(data)
        {
            Scale = data.scale;
        }


        
        public override void Play()
        {
            TimeManager.SetTimeScale(Scale);
        }



        public float Scale { get; set; } = 1.0f;
    }



    [Serializable]
    [GameEvent(typeof(GameEventSetTimeScale))]
    public class GameEventSetTimeScaleData : GameEventData
    {
        public float scale;
    }
}