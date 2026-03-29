using System;
using System.Collections;
using Rune.UI;



namespace Rune
{
    public class GameEventCloseAllHuds : GameEvent
    {
        public GameEventCloseAllHuds() : base()
        {
            
        }

        public GameEventCloseAllHuds(GameEventCloseAllModalsData data) : base(data)
        {
            
        }



        public override IEnumerator Play()
        {
            yield return IUIHud.TransitionOutAllAndWait();
        }
    }



    [Serializable]
    [GameEvent(typeof(GameEventCloseAllHuds))]
    public class GameEventCloseAllHudsData : GameEventData
    {
        
    }
}