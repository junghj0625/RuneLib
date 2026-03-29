using System.Collections.Generic;



namespace Rune
{
    public class GameEventWhile : GameEvent
    {
        public List<GameEvent> GameEvents { get; set; } = null;
    }
}