using UnityEngine;



namespace Rune.UI
{
    public interface IUIDecidable
    {
        public void Decide();



        public LooseEvent OnDecide { get; }
    }



    /* Example of IUIDecidable */

    public class IUIDecidableExample : UIObject, IUIDecidable
    {
        public void Decide()
        {
            Animator.SetTrigger("Decide");

            OnDecide.Invoke();
        }



        private Animator _animator = null;
        private Animator Animator { get => LazyGetComponent(ref _animator); }



        public LooseEvent OnDecide { get; } = new();
    }
}