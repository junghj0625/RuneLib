using System.Collections;
using System.Collections.Generic;



namespace Rune
{
    public interface IPreloadable
    {
        public static void AddPreloadable(IPreloadable preloadable)
        {
            _preloadables.Add(preloadable);
        }

        public static void RemovePreloadable(IPreloadable preloadable)
        {
            _preloadables.Remove(preloadable);
        }

        public static IEnumerator PreloadAll()
        {
            foreach (var preloadable in _preloadables)
            {
                if (preloadable.IsPreloaded) continue;

                yield return preloadable.Preload();
            }
        }



        public IEnumerator Preload();



        public bool IsPreloaded { get; set; }



        private static readonly HashSet<IPreloadable> _preloadables = new();
    }



    /* Example of IPreloadable */

    public class IPreloadableExample : MonoPlus, IPreloadable
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


        public IEnumerator Preload()
        {
            /* Do something */

            yield break;
        }



        public bool IsPreloaded { get; set; } = false;
    }
}