using System.Collections;



namespace Rune
{
    public abstract partial class Stage : MonoPlus
    {
        public sealed override void Start()
        {
            StartCoroutine(StartRoutine());
        }

        public virtual void LateStart()
        {

        }



        private IEnumerator StartRoutine()
        {
            yield return null;

            LateStart();
        }
    }



    /* Example of Stage */

    public class StageExample : Stage
    {
        public override void LateStart()
        {
            base.LateStart();

            GameEventPlayer.Schedule
            (
                new GameEventCustomAction { ScheduledAction = () => {} },
                new GameEventWait { Duration = 1.0f }
            );
        }
    }
}