using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Rune
{
    public class GameEventPlayer : MonoPlusSingleton<GameEventPlayer>
    {
        public static void Schedule(out ScheduleContext context, List<GameEvent> gameEvents)
        {
            context = null;

            if (SingletonInstance == null || !SingletonInstance.gameObject.activeInHierarchy || gameEvents == null) return;

            context = new ScheduleContext { IsPlaying = true };

            SingletonInstance.StartCoroutine(StartSchedule(gameEvents, context));
        }

        public static void Schedule(out ScheduleContext context, params GameEvent[] gameEvents)
        {
            Schedule(out context, new List<GameEvent>(gameEvents));
        }

        public static void Schedule(List<GameEvent> gameEvents)
        {
            Schedule(out _, gameEvents);
        }

        public static void Schedule(params GameEvent[] gameEvents)
        {
            Schedule(out _, gameEvents);
        }


        public static IEnumerator ScheduleAndWait(List<GameEvent> gameEvents)
        {
            Schedule(out var context, gameEvents);

            if (context == null) yield break;

            yield return new WaitUntil(() => !context.IsPlaying);
        }

        public static IEnumerator ScheduleAndWait(params GameEvent[] gameEvents)
        {
            yield return ScheduleAndWait(new List<GameEvent>(gameEvents));
        }


        
        private static IEnumerator StartSchedule(List<GameEvent> gameEvents, ScheduleContext context)
        {
            context.IsPlaying = true;

            yield return ProcessGameEvents(Guid.NewGuid().ToString(), gameEvents, new());

            context.IsPlaying = false;
        }

        private static IEnumerator ProcessGameEvents(string ID, List<GameEvent> gameEvents, ProcessContext context)
        {
            if (string.IsNullOrEmpty(ID) || gameEvents == null) yield break;

            context.CurrentState = ProcessContextState.Normal;


            // Run game events
            for (int i = 0; i < gameEvents.Count; i++)
            {
                // Get game event
                var gameEvent = gameEvents[i];

                if (gameEvent == null || !gameEvent.IsPlayable) continue;


                switch (gameEvent)
                {
                    // Block
                    case GameEventBlock gameEventBlock:

                        yield return ProcessGameEvents(Guid.NewGuid().ToString(), gameEventBlock.GameEvents, context);

                        break;

                    
                    case GameEventBlockByKey gameEventBlockByKey:

                        if (GameEventSetPool.TryGet(gameEventBlockByKey.Key, out var gameEventSet))
                        {
                            yield return ProcessGameEvents(Guid.NewGuid().ToString(), gameEventSet.gameEvents, context);
                        }
                        else
                        {
                            DebugManager.Log($"Game event set not found. ({gameEventBlockByKey.Key})");
                        }

                        break;


                    // If
                    case GameEventIf gameEventIf:

                        if (gameEventIf.Result)
                        {
                            yield return ProcessGameEvents(Guid.NewGuid().ToString(), gameEventIf.TrueThen, context);
                        }
                        else
                        {
                            yield return ProcessGameEvents(Guid.NewGuid().ToString(), gameEventIf.FalseThen, context);
                        }

                        break;


                    // Switch
                    case GameEventSwitch gameEventSwitch:

                        foreach (var option in gameEventSwitch.Options)
                        {
                            if (option.Predicate != null && option.Predicate())
                            {
                                yield return ProcessGameEvents(Guid.NewGuid().ToString(), option.Block, context);

                                break;
                            }
                        }

                        break;


                    // While
                    case GameEventWhile gameEventWhile:

                        string whileID = Guid.NewGuid().ToString();

                        context.WhileIDStack.Push(whileID);

                        yield return ProcessGameEvents(whileID, gameEventWhile.GameEvents, context);

                        context.WhileIDStack.Pop();

                        break;


                    // Play
                    case GameEventAction gameEventAction:

                        gameEventAction.Play();

                        break;


                    // Continue
                    case GameEventContinue gameEventContinue:

                        context.CurrentState = ProcessContextState.Continue;

                        break;


                    // Break
                    case GameEventBreak gameEventBreak:

                        context.CurrentState = ProcessContextState.Break;

                        break;


                    default:

                        yield return gameEvent.Play();

                        break;
                }


                // Continue state
                if (context.CurrentState == ProcessContextState.Continue)
                {
                    if (ID == context.CurrentWhileID)
                    {
                        i = -1;

                        context.CurrentState = ProcessContextState.Normal;
                    }
                    else
                    {
                        break;
                    }
                }


                // Break state
                if (context.CurrentState == ProcessContextState.Break)
                {
                    if (ID == context.CurrentWhileID)
                    {
                        context.CurrentState = ProcessContextState.Normal;
                    }

                    break;
                }
            }
        }



        public class ProcessContext
        {
            public string CurrentWhileID
            {
                get => WhileIDStack.Count == 0 ? null : WhileIDStack.Peek();
            }



            public Stack<string> WhileIDStack { get; } = new();

            public ProcessContextState CurrentState { get; set; } = ProcessContextState.Normal;
        }



        public enum ProcessContextState
        {
            Normal,
            Continue,
            Break,
        }
    
    
    
        public class ScheduleContext
        {
            public bool IsPlaying { get; set; } = false;
        }
    }
}