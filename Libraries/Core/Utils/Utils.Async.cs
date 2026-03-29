// Date: 2025-07-28



using System;
using System.Collections;
using System.Threading.Tasks;



namespace Rune.Utils
{
    public readonly struct Async
    {
        public static IEnumerator WaitForTask(Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                DebugManager.Log(task.Exception);

                yield break;
            }
        }

        public static IEnumerator WaitForTask<T>(Task<T> task, Action<T> onComplete)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                DebugManager.Log(task.Exception);

                yield break;
            }

            onComplete?.Invoke(task.Result);
        }
    }
}