namespace Rune
{
    public class GameEventRemoveParameter : GameEventAction
    {
        public override void Play()
        {
            ParameterManager.Parameters.RemoveParameter(Key);
        }



        public string Key { get; set; }
    }
}