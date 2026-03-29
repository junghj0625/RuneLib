using System;
using System.Collections;
using System.Collections.Generic;



namespace Rune
{
    public abstract class KeyedDataPreloader : MonoPlusSingleton<KeyedDataPreloader>, IPreloadable
    {
        public override void OnEnable()
        {
            base.OnEnable();

            IPreloadable.AddPreloadable(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            IPreloadable.RemovePreloadable(this);
        }



        public abstract IEnumerator Preload();



        public bool IsPreloaded { get; set; } = false;
    }
}