using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Rune.Utils
{
    public readonly struct Coroutine
    {
        public static UnityEngine.Coroutine Run(List<CoroutineCommand> commands)
        {
            return Run(DefaultOwner, commands);
        }

        public static UnityEngine.Coroutine Run(MonoBehaviour mono, List<CoroutineCommand> commands)
        {
            if (mono == null) return null;

            return mono.StartCoroutine(Running(commands));
        }

        public static UnityEngine.Coroutine Run(params CoroutineCommand[] commands)
        {
            return Run(DefaultOwner, commands);
        }

        public static UnityEngine.Coroutine Run(MonoBehaviour mono, params CoroutineCommand[] commands)
        {
            return Run(mono, new List<CoroutineCommand>(commands));
        }

        public static UnityEngine.Coroutine Run(List<IEnumerator> coroutines)
        {
            return Run(DefaultOwner, coroutines);
        }

        public static UnityEngine.Coroutine Run(MonoBehaviour mono, List<IEnumerator> coroutines)
        {
            var commands = new List<CoroutineCommand>();

            foreach (var coroutine in coroutines) commands.Add(new CoroutineCommandCoroutine { FuncGetScheduledCoroutine = () => coroutine });

            return Run(mono, commands);
        }

        public static UnityEngine.Coroutine Run(params IEnumerator[] coroutines)
        {
            return Run(DefaultOwner, coroutines);
        }

        public static UnityEngine.Coroutine Run(MonoBehaviour mono, params IEnumerator[] coroutines)
        {
            return Run(mono, new List<IEnumerator>(coroutines));
        }

        public static IEnumerator Running(List<CoroutineCommand> commands)
        {
            foreach (var command in commands) yield return command.Run();
        }

        public static IEnumerator Running(params CoroutineCommand[] commands)
        {
            yield return Running(new List<CoroutineCommand>(commands));
        }



        public abstract class CoroutineCommand
        {
            public abstract IEnumerator Run();
        }



        public class CoroutineCommandAction : CoroutineCommand
        {
            public override IEnumerator Run()
            {
                if (ScheduledAction == null) yield break;

                ScheduledAction();

                yield break;
            }



            public Action ScheduledAction { get; set; } = null;
        }



        public class CoroutineCommandCoroutine : CoroutineCommand
        {
            public override IEnumerator Run()
            {
                if (FuncGetScheduledCoroutine == null) yield break;

                var coroutine = FuncGetScheduledCoroutine();

                if (coroutine == null) yield break;

                yield return coroutine;

                yield break;
            }



            public Func<IEnumerator> FuncGetScheduledCoroutine { get; set; } = null;
        }



        public class CoroutineCommandParallel : CoroutineCommand
        {
            public override IEnumerator Run()
            {
                MonoBehaviour owner = Owner != null ? Owner : DefaultOwner;

                if (owner == null) yield break;

                if (FuncGetScheduledCoroutines == null) yield break;


                var coroutines = FuncGetScheduledCoroutines();

                var runningCoroutines = new List<UnityEngine.Coroutine>();

                var completedFlags = new List<bool>();


                for (int i = 0; i < coroutines.Count; i++)
                {
                    completedFlags.Add(false);

                    int index = i;


                    IEnumerator Wrapper()
                    {
                        yield return coroutines[index];

                        completedFlags[index] = true;
                    }


                    UnityEngine.Coroutine coroutine = owner.StartCoroutine(Wrapper());

                    runningCoroutines.Add(coroutine);
                }


                yield return new WaitUntil(() => completedFlags.TrueForAll(done => done));
            }



            public MonoBehaviour Owner { get; set; } = null;

            public Func<List<IEnumerator>> FuncGetScheduledCoroutines { get; set; } = null;
        }



        public class CoroutineCommandWaitUntilOrTimeout : CoroutineCommand
        {
            public override IEnumerator Run()
            {
                float timer = 0.0f;

                while (timer < Timeout)
                {
                    if (Predicate()) yield break;

                    timer += Time.deltaTime;

                    yield return null;
                }
            }



            public float Timeout { get; set; } = 0.0f;

            public Func<bool> Predicate { get; set; } = null;
        }



        public static MonoBehaviour DefaultOwner { get; set; } = null;
    }
}