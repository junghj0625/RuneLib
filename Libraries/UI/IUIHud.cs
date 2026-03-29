using System.Collections;
using System.Collections.Generic;
using System.Linq;



namespace Rune.UI
{
    public interface IUIHud
    {
        public static void Register(IUIHud hud)
        {
            _huds.Add(hud);
        }

        public static void Unregister(IUIHud hud)
        {
            _huds.Remove(hud);
        }


        public static IEnumerator TransitionOutAllAndWait()
        {
            foreach (var hud in _huds.ToList())
            {
                if (hud is not IUITransitionable transitionable) continue;

                yield return IUITransitionable.StartTransitionOutAndWait(transitionable);
            }
        }



        private static readonly HashSet<IUIHud> _huds = new();
    }



    /* Example of IUIHud */

    public class IUIHudExample : UIObject, IUIHud
    {
        public override void OnEnable()
        {
            base.OnEnable();

            transform.SetAsLastSibling();

            IUIHud.Register(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            
            IUIHud.Unregister(this);
        }
    }
}