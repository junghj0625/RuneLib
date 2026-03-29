using UnityEngine;



namespace Rune.UI
{
    public interface IUIFreezable
    {
        public void Freeze();
        public void Defreeze();



        public bool Frozen { get; }

        public LooseEvent OnFreeze { get; }
        public LooseEvent OnDefreeze { get; }
    }



    /* Example of IUIFreezable */

    public class IUIFreezableExample : UIObject, IUIFreezable
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _frozen.OnChange.AddListener(OnChangeFrozen);
        }

        public override void Refresh()
        {
            base.Refresh();

            _frozen.Refresh();
        }


        public void Freeze()
        {
            _frozen.Value = true;

            OnFreeze.Invoke();
        }

        public void Defreeze()
        {
            _frozen.Value = false;

            OnDefreeze.Invoke();
        }



        public bool Frozen
        {
            get => _frozen.Value;
        }



        public LooseEvent OnFreeze { get; } = new();
        public LooseEvent OnDefreeze { get; } = new();



        private void OnChangeFrozen(bool value)
        {
            Animator.SetBool("Freeze", value);
        }



        private Animator _animator = null;
        private Animator Animator { get => LazyGetComponent(ref _animator); }



        private readonly Attribute<bool> _frozen = new(false);
    }
}