using System;
using System.Collections;



namespace Rune
{
    public class GameEventLog : GameEvent
    {
        public GameEventLog() : base()
        {
            
        }

        public GameEventLog(GameEventLogData data) : base(data)
        {
            Message = data.message;
        }



        public override IEnumerator Play()
        {
            DebugManager.Log(Message);

            yield break;
        }



        public string Message { get; set; } = string.Empty;
    }



    [Serializable]
    [GameEvent(typeof(GameEventLog))]
    public class GameEventLogData : GameEventData
    {
        public string message;
    }
}