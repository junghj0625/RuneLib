namespace Rune
{
    public abstract class GameEventAction : GameEvent
    {
        public GameEventAction() : base()
        {
            
        }

        public GameEventAction(GameEventData data) : base(data)
        {
            
        }



        public abstract new void Play();
    }
}