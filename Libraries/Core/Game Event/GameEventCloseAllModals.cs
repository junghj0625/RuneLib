using System;
using System.Collections;
using Rune.UI;



namespace Rune
{
    public class GameEventCloseAllModals : GameEvent
    {
        public GameEventCloseAllModals() : base()
        {
            
        }

        public GameEventCloseAllModals(GameEventCloseAllModalsData data) : base(data)
        {
            
        }



        public override IEnumerator Play()
        {
            yield return IUIModal.TransitionOutAllAndWait();
        }
    }



    [Serializable]
    [GameEvent(typeof(GameEventCloseAllModals))]
    public class GameEventCloseAllModalsData : GameEventData
    {
        
    }
}