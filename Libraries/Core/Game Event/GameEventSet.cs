using System.Collections.Generic;



namespace Rune
{
    public class GameEventSet : KeyedData
    {
        public static GameEventSet FromSO(GameEventDataSetSO SO)
        {
            if (SO == null) return new();

            return new()
            {
                Key = SO.key,
                
                gameEvents = SO.gameEvents.ConvertAll(ge => ge != null ? GameEvent.FromData(ge) : null),
            };
        }



        public List<GameEvent> gameEvents = new();
    }
}