using System;



namespace Rune
{
    public class GameEventCustomAction : GameEventAction
    {
        public override void Play()
        {
            ScheduledAction?.Invoke();
        }



        public Action ScheduledAction { get; set; } = null;
    }
}