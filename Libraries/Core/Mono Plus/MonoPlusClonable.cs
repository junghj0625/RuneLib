namespace Rune
{
    public abstract class MonoPlusClonable<T> : MonoPlus where T : MonoPlusClonable<T>
    {
        public sealed override void Awake()
        {
            base.Awake();

            if (TemplateInstance == null)
            {
                TemplateInstance = this as T;

                OnAwakeTemplate();
            }

            OnAwake();
        }

        public sealed override void Start()
        {
            base.Start();

            if (TemplateInstance == this)
            {
                TemplateInstance.gameObject.SetActive(false);

                OnStartTemplate();
            }
            
            OnStart();
        }


        public virtual void OnAwake()
        {
            /* Noop */
        }

        public virtual void OnAwakeTemplate()
        {
            /* Noop */
        }

        public virtual void OnStart()
        {
            /* Noop */
        }

        public virtual void OnStartTemplate()
        {
            /* Noop */
        }


        public static T Clone()
        {
            var instance = Instantiate(TemplateInstance.gameObject, TemplateInstance.gameObject.transform.parent).GetComponent<T>();
        
            instance.gameObject.SetActive(true);

            return instance;
        }



        public static T TemplateInstance { get; protected set; } = null;
    }
}