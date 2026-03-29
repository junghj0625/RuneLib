// Title: CoroutineQueue.cs
// Version: 1.0.0
// Date: 2025-06-05



using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Rune
{
    public class CoroutineQueue
    {
        public CoroutineQueue(MonoBehaviour mono)
        {
            _mono = mono;
        }



        public void Schedule(Utils.Coroutine.CoroutineCommand command)
        {
            if (_mono == null) return;

            _commands.Enqueue(command);

            if (!isRunning)
            {
                isRunning = true;
                
                _mono.StartCoroutine(ProcessQueue());
            }
        }



        private IEnumerator ProcessQueue()
        {
            isRunning = true;

            while (_commands.Count > 0)
            {
                var command = _commands.Dequeue();

                if (command != null) yield return command.Run();
            }

            isRunning = false;

            yield break;
        }



        private readonly MonoBehaviour _mono = null;

        private readonly Queue<Utils.Coroutine.CoroutineCommand> _commands = new();

        private bool isRunning = false;
    }
}