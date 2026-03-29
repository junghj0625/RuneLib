using System;



namespace Rune
{
    [Serializable]
    public abstract class GameEventData : IGameEventData
    {
        public bool isActive = true;
    }
}