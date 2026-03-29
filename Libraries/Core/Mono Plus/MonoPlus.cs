using System;
using System.Collections.Generic;
using UnityEngine;



namespace Rune
{
    public abstract class MonoPlus : MonoBehaviour
    {
        public virtual void Reset()
        {
            /* Noop */
        }

        public virtual void Awake()
        {
            InitModules();
            InitCollection();
            InitObjects();
            Init();
        }

        public virtual void Start()
        {
            foreach (var module in _modules) module.Start();
        }

        public virtual void Update()
        {
            foreach (var module in _modules) module.Update();
        }

        public virtual void FixedUpdate()
        {
            foreach (var module in _modules) module.FixedUpdate();
        }

        public virtual void LateUpdate()
        {
            foreach (var module in _modules) module.LateUpdate();
        }

        public virtual void OnEnable()
        {
            foreach (var module in _modules) module.OnEnable();
        }

        public virtual void OnDisable()
        {
            foreach (var module in _modules) module.OnDisable();
        }

        public virtual void OnDestroy()
        {
            foreach (var module in _modules) module.OnDestroy();
        }


        /// <summary>
        /// Initializes the components.
        /// </summary>
        public virtual void InitModules()
        {

        }

        /// <summary>
        /// Initializes the collections.
        /// </summary>
        public virtual void InitCollection()
        {
            foreach (var module in _modules) module.InitCollection();
        }

        /// <summary>
        /// Initializes all Unity-related objects.
        /// </summary>
        public virtual void InitObjects()
        {
            foreach (var module in _modules) module.InitObjects();
        }

        /// <summary>
        /// Performs the final initialization steps.
        /// </summary>
        public virtual void Init()
        {
            foreach (var module in _modules) module.Init();
        }

        /// <summary>
        /// Refreshes the object.
        /// </summary>
        public virtual void Refresh()
        {
            foreach (var module in _modules) module.Refresh();
        }


        public T LazyGetComponent<T>(ref T cache) where T : Component
        {
            if (this == null || transform == null || transform.gameObject == null) { cache = null; return null; }

            if (cache == null) cache = transform.GetComponent<T>();

            return cache;
        }

        public T LazyGetComponentInChildren<T>(ref T cache) where T : Component
        {
            if (this == null || transform == null || transform.gameObject == null) { cache = null; return null; }

            if (cache == null) cache = transform.GetComponentInChildren<T>();

            return cache;
        }

        public T LazyGetComponentFromPath<T>(ref T cache, string path) where T : Component
        {
            if (this == null || transform == null || transform.gameObject == null) { cache = null; return null; }

            if (cache == null) cache = transform.Find(path).GetComponent<T>();

            return cache;
        }

        public GameObject LazyGetGameObjectFromPath(ref GameObject cache, string path)
        {
            if (this == null || transform == null || transform.gameObject == null) { cache = null; return null; }

            if (cache == null) cache = transform.Find(path).gameObject;

            return cache;
        }

        public static T LazyGetComponentFromTransform<T>(ref T cache, Transform transform) where T : Component
        {
            if (transform == null || transform.gameObject == null) { cache = null; return null; }

            if (cache == null) cache = transform.GetComponent<T>();

            return cache;
        }

        public static GameObject LazyGetGameObjectFromTransform(ref GameObject cache, Transform transform)
        {
            if (transform == null || transform.gameObject == null) { cache = null; return null; }

            if (cache == null) cache = transform.gameObject;

            return cache;
        }



        public string ID { get; } = Guid.NewGuid().ToString();



        protected void AddModule(MonoPlusModule module)
        {
            module.Owner = this;

            _modules.Add(module);
        }

        protected void RemoveModule(MonoPlusModule module)
        {
            module.Owner = null;
            
            _modules.Remove(module);
        }



        protected HashSet<MonoPlusModule> _modules = new();
    }
}