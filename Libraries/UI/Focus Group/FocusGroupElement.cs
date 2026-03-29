namespace Rune.UI
{
    public class FocusGroupElement : FocusGroupItem
    {
        public override void Focus()
        {
            base.Focus();

            _focusable.Focus();
        }

        public override void Defocus()
        {
            base.Defocus();

            _focusable.Defocus();
        }



        public override IUIFocusable CurrentFocusable
        {
            get => _focusable;
        }



        public IUIFocusable Focusable
        {
            get => _focusable;
            set => _focusable = value;
        }


        
        private IUIFocusable _focusable = null;
    }
}