using System;
using System.Collections.Generic;



namespace Rune
{
    public class GameEventBlock : GameEvent
    {
        public GameEventBlock() : base()
        {
            
        }

        public GameEventBlock(List<GameEvent> gameEvents) : base()
        {
            GameEvents = new(gameEvents);
        }

        public GameEventBlock(GameEventBlockBySOData data) : this(GameEventSet.FromSO(data.gameEventDataSetSO).gameEvents)
        {
            
        }



        public List<GameEvent> GameEvents { get; set; } = new();
    }



    [Serializable]
    [GameEvent(typeof(GameEventBlock))]
    public class GameEventBlockBySOData : GameEventData
    {
        public GameEventDataSetSO gameEventDataSetSO;
    }
}