using System.Collections;
using UnityEngine;



namespace Rune
{
    public class RoutinePlayer
    {
        public void Run(MonoBehaviour owner, IEnumerator routine)
        {
            Stop();


            if (owner == null)
            {
                DebugManager.Log("Owner is null.");

                return;
            }


            _owner = owner;


            if (routine != null)
            {
                _coroutineHandle = _owner.StartCoroutine(RunRoutine(routine));
            }


            IEnumerator RunRoutine(IEnumerator routine)
            {
                /* 코루틴 래퍼는 반드시 직접 루틴을 컨트롤해야 합니다. */

                while (routine.MoveNext())
                {
                    yield return routine.Current;
                }

                _coroutineHandle = null;
            }
        }

        public IEnumerator RunAndWait(MonoBehaviour owner, IEnumerator routine)
        {
            Run(owner, routine);

            if (IsRunning) yield return _coroutineHandle;
        }

        public void Stop()
        {
            if (_owner != null && _coroutineHandle != null)
            {
                _owner.StopCoroutine(_coroutineHandle);
            }

            _coroutineHandle = null;

            _owner = null;
        }



        public bool IsRunning
        {
            get => _coroutineHandle != null;
        }



        private MonoBehaviour _owner = null;

        private Coroutine _coroutineHandle = null;
    }



    /* Example of RoutinePlayer user */

    public class RoutinePlayerUserExample : MonoPlus
    {
        public void Play()
        {
            _routinePlayer.Run(this, Routine());
        }

        public void Stop()
        {
            _routinePlayer.Stop();
        }



        private IEnumerator Routine()
        {
            /* Do something */

            yield break;
        }



        private readonly RoutinePlayer _routinePlayer = new();
    }
}