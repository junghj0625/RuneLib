using System;



namespace Rune
{
    public class GameEventBlockByKey : GameEvent
    {
        public GameEventBlockByKey() : base()
        {
            
        }

        public GameEventBlockByKey(string key) : base()
        {
            Key = key;
        }

        public GameEventBlockByKey(GameEventBlockByKeyData data) : this(data.key)
        {
            
        }



        public string Key { get; set; } = string.Empty;
    }



    [Serializable]
    [GameEvent(typeof(GameEventBlockByKey))]
    public class GameEventBlockByKeyData : GameEventData
    {
        public string key;
    }
}