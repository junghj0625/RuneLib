using UnityEngine;



namespace Rune.UI
{
    public interface IUIFocusable
    {
        public void Focus();
        public void Defocus();



        public bool Focused { get; }

        public LooseEvent OnFocus { get; }
        public LooseEvent OnDefocus { get; }
    }



    /* Example of IUIFocusable */

    public class IUIFocusableExample : UIObject, IUIFocusable
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _focused.OnChange.AddListener(OnChangeFocused);
        }

        public override void Refresh()
        {
            base.Refresh();

            _focused.Refresh();
        }


        public void Focus()
        {
            _focused.Value = true;

            OnFocus.Invoke();
        }

        public void Defocus()
        {
            _focused.Value = false;

            OnDefocus.Invoke();
        }



        public bool Focused
        {
            get => _focused.Value;
        }



        public LooseEvent OnFocus { get; } = new();
        public LooseEvent OnDefocus { get; } = new();



        private void OnChangeFocused(bool value)
        {
            Animator.SetBool("Focus", value);
        }



        private Animator _animator = null;
        private Animator Animator { get => LazyGetComponent(ref _animator); }



        private readonly Attribute<bool> _focused = new(false);
    }
}