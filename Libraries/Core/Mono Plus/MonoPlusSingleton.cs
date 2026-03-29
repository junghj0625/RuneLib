namespace Rune
{
    public abstract class MonoPlusSingleton<T> : MonoPlus where T : MonoPlusSingleton<T>
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

        }

        public virtual void OnStart()
        {

        }



        public static T SingletonInstance { get; protected set; } = null;
    }
}