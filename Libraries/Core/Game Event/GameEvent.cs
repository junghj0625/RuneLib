using System;
using System.Collections;



namespace Rune
{
    public abstract class GameEvent
    {
        public GameEvent()
        {
            
        }

        public GameEvent(GameEventData data)
        {
            IsActive = data.isActive;
        }



        public virtual IEnumerator Play()
        {
            /* Noop */
            
            yield break;
        }


        public static GameEvent FromData(GameEventData data)
        {
            return GameEventFactory.Create(data);
        }



        public bool IsPlayable
        {
            get => IsActive && Predicate();
        }



        public Func<bool> Predicate { get; set; } = () => true;

        public bool IsActive { get; set; } = true;
    }



    /* Example of GameEvent */

    public class GameEventExample : GameEvent
    {
        public override IEnumerator Play()
        {
            /* Do something */

            yield break;
        }
    }
}