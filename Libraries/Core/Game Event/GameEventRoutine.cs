using System.Collections;



namespace Rune
{
    public class GameEventRoutine : GameEvent
    {
        public override IEnumerator Play()
        {
            if (ScheduledRoutine != null) yield return ScheduledRoutine;
        }



        public IEnumerator ScheduledRoutine { get; set; } = null;
    }
}