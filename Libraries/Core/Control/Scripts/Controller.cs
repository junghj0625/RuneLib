namespace Rune.Controls
{
    public abstract class Controller : MonoPlusModule
    {
        public Registry<MonoPlus> InputBlocker { get; } = new();

        public bool Controllable { get; set; } = true;



        protected abstract bool IsFinallyControllable { get; }
    }



    public abstract class Controller<T> : Controller where T : Controller<T>
    {
        public static Registry<MonoPlus> GlobalInputBlocker { get; } = new();
    }



    /* Example of Controller */

    public class ControllerExample : Controller<ControllerExample>
    {
        public void EnableControl()
        {
            ControlManager.RegisterController(this);
        }

        public void DisableControl()
        {
            ControlManager.UnregisterController(this);
        }



        protected override bool IsFinallyControllable
        {
            get => !GlobalInputBlocker.IsRegistered && !InputBlocker.IsRegistered && Controllable;
        }
    }
}