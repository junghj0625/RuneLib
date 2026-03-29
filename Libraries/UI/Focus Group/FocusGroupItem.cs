namespace Rune.UI
{
    public abstract class FocusGroupItem
    {
        public virtual void Focus()
        {
            /* Noop */
        }

        public virtual void Defocus()
        {
            /* Noop */
        }


        
        public IUIFocusable GetCurrentFocusable()
        {
            return CurrentFocusable;
        }

        public T GetCurrentFocusable<T>() where T : class, IUIFocusable
        {
            return CurrentFocusable as T;
        }



        public abstract IUIFocusable CurrentFocusable { get; }
    }
}