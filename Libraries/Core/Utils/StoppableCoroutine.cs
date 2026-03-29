// Title: StoppableCoroutine.cs
// Version: 1.0.1
// Date: 2025-06-12



using System.Collections;
using UnityEngine;



namespace Rune
{
    public class StoppableCoroutine
    {
        public void Run(IEnumerator routine)
        {
            if (Owner == null) return;

            Stop();

            _coroutine = Owner.StartCoroutine(Wrap(routine));
        }

        public void Stop()
        {
            if (Owner == null || _coroutine == null) return;

            Owner.StopCoroutine(_coroutine);

            _coroutine = null;
        }



        public bool IsRunning
        {
            get => _coroutine != null;
        }



        public MonoBehaviour Owner { get; set; } = null;



        private IEnumerator Wrap(IEnumerator routine)
        {
            yield return routine;

            _coroutine = null;
        }



        private Coroutine _coroutine;
    }



    /* How to use StoppableCoroutine */

    public class StoppableCoroutineUserExample : MonoBehaviour
    {
        public void Start()
        {
            stoppableCoroutine.Owner = this;
            stoppableCoroutine.Run(Routine());
            stoppableCoroutine.Stop();
        }


        public IEnumerator Routine()
        {
            yield break;
        }



        private readonly StoppableCoroutine stoppableCoroutine = new();
    }
}