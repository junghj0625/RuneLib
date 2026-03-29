using System;
using System.Collections.Generic;



namespace Rune
{
    public class GameEventSwitch : GameEvent
    {
        public List<Option> Options { get; set; } = new();



        public class Option
        {
            public Func<bool> Predicate { get; set; } = null;

            public List<GameEvent> Block { get; set; } = new();
        }
    }
}