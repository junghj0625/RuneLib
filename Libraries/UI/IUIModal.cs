using System.Collections;
using System.Collections.Generic;
using System.Linq;



namespace Rune.UI
{
    public interface IUIModal
    {
        public static void Register(IUIModal modal)
        {
            if (_modals.Add(modal)) _modalOrder.Add(modal);
        }

        public static void Unregister(IUIModal modal)
        {
            if (_modals.Remove(modal)) _modalOrder.Remove(modal);
        }
        

        public static IEnumerator TransitionOutAllAndWait()
        {
            foreach (var modal in _modalOrder.AsEnumerable().Reverse())
            {
                if (modal is not IUITransitionable transitionable) continue;
                
                yield return IUITransitionable.StartTransitionOutAndWait(transitionable);
            }
        }



        private static readonly HashSet<IUIModal> _modals = new();

        private static readonly List<IUIModal> _modalOrder = new();
    }



    /* Example of IUIModal */

    public class IUIModalExample : UIObject, IUIModal
    {
        public override void OnEnable()
        {
            base.OnEnable();

            IUIModal.Register(this);

            transform.SetAsLastSibling();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            
            IUIModal.Unregister(this);
        }
    }
}