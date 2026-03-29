using System;



namespace Rune
{
    public abstract class MonoPlusModule
    {
        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void LateUpdate()
        {

        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnDestroy()
        {

        }

        /// <summary>
        /// Initializes the collections.
        /// </summary>
        public virtual void InitCollection()
        {

        }

        /// <summary>
        /// Initializes all Unity-related objects.
        /// </summary>
        public virtual void InitObjects()
        {

        }

        /// <summary>
        /// Performs the final initialization steps.
        /// </summary>
        public virtual void Init()
        {

        }

        /// <summary>
        /// Refreshes the object.
        /// </summary>
        public virtual void Refresh()
        {

        }



        public string ID { get; } = Guid.NewGuid().ToString();

        public MonoPlus Owner { get; set; } = null;
    }



    public abstract class MonoPlusModule<TOwner> : MonoPlusModule where TOwner : MonoPlus
    {
        public new TOwner Owner
        {
            get => base.Owner as TOwner;
            set => base.Owner = value;
        }
    }
}