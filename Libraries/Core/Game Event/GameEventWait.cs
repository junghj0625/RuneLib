using System;
using System.Collections;
using UnityEngine;



namespace Rune
{
    public class GameEventWait : GameEvent
    {
        public GameEventWait() : base()
        {
            
        }

        public GameEventWait(GameEventWaitData data) : base(data)
        {
            Duration = data.duration;
        }



        public override IEnumerator Play()
        {
            yield return new WaitForSeconds(Duration);
        }



        public float Duration { get; set; } = 0.0f;
    }



    [Serializable]
    [GameEvent(typeof(GameEventWait))]
    public class GameEventWaitData : GameEventData
    {
        public float duration;
    }
}