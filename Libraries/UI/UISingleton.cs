namespace Rune.UI
{
    public abstract class UISingleton<T> : UIObject where T : UISingleton<T>
    {
        public sealed override void Awake()
        {
            base.Awake();

            if (SingletonInstance == null)
            {
                SingletonInstance = this as T;
            }
            else
            {
                return;
            }

            OnAwake();
        }

        public sealed override void Start()
        {
            base.Start();

            if (SingletonInstance != this)
            {
                Destroy(this);

                return;
            }

            OnStart();
        }


        public virtual void OnAwake()
        {
            /* Noop */
        }

        public virtual void OnStart()
        {
            /* Noop */
        }



        public static T SingletonInstance { get; protected set; } = null;
    }



    /* Example of UISingleton */
    
    public class UISingletonExample : UISingleton<UISingletonExample>
    {
        public override void OnStart()
        {
            base.OnStart();

            gameObject.SetActive(false);
        }
    }
}