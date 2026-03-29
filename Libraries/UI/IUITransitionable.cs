using System.Collections;
using UnityEngine;



namespace Rune.UI
{
    public interface IUITransitionable
    {
        public IEnumerator TransitionInRoutine();
        public IEnumerator TransitionOutRoutine();



        public bool IsTransitioning { get; set; }

        public RoutinePlayer TransitionRoutine { get; }



        public static IEnumerator StartTransitionInAndWait(IUITransitionable transitionable)
        {
            transitionable.IsTransitioning = true;

            StartTransitionInRoutine(transitionable);

            yield return new WaitUntil(() => !transitionable.IsTransitioning);
        }

        public static IEnumerator StartTransitionOutAndWait(IUITransitionable transitionable)
        {
            transitionable.IsTransitioning = true;

            StartTransitionOutRoutine(transitionable);

            yield return new WaitUntil(() => !transitionable.IsTransitioning);
        }
        


        protected static void StartTransitionInRoutine(IUITransitionable transitionable)
        {
            if (transitionable is null) return;

            if (transitionable is not MonoBehaviour mono || mono == null || mono.gameObject == null)
            {
                transitionable.IsTransitioning = false;

                transitionable.TransitionRoutine.Stop();

                return;
            }


            mono.gameObject.SetActive(true);

            transitionable.IsTransitioning = true;

            transitionable.TransitionRoutine.Run(mono, transitionable.TransitionInRoutine());
        }

        protected static void StartTransitionOutRoutine(IUITransitionable transitionable)
        {
            if (transitionable is null) return;

            if (transitionable is not MonoBehaviour mono || mono == null || mono.gameObject == null)
            {
                transitionable.IsTransitioning = false;

                transitionable.TransitionRoutine.Stop();

                return;
            }


            mono.gameObject.SetActive(true);

            transitionable.IsTransitioning = true;
            
            transitionable.TransitionRoutine.Run(mono, transitionable.TransitionOutRoutine());
        }
    }



    /* Example of IUITransitionable */

    public class IUITransitionableExample : UIObject, IUITransitionable
    {
        public override void OnDisable()
        {
            base.OnDisable();

            IsTransitioning = false;
        }


        IEnumerator IUITransitionable.TransitionInRoutine()
        {
            IsTransitioning = true;

            gameObject.SetActive(true);

            Refresh();

            IsTransitioning = false;

            yield break;
        }

        IEnumerator IUITransitionable.TransitionOutRoutine()
        {
            IsTransitioning = true;

            gameObject.SetActive(false);

            IsTransitioning = false;

            yield break;
        }


        public void Open()
        {
            IUITransitionable.StartTransitionInRoutine(this);
        }

        public void Close()
        {
            IUITransitionable.StartTransitionOutRoutine(this);
        }

        public IEnumerator OpenAndWait()
        {
            yield return IUITransitionable.StartTransitionInAndWait(this);
        }

        public IEnumerator CloseAndWait()
        {
            yield return IUITransitionable.StartTransitionOutAndWait(this);
        }



        public bool IsTransitioning { get; set; } = false;

        public RoutinePlayer TransitionRoutine { get; } = new();
    }



    /* Example of IUITransitionable singleton */

    public class IUITransitionableSingletonExample : UISingleton<IUITransitionableSingletonExample>, IUITransitionable
    {
        public override void OnDisable()
        {
            base.OnDisable();

            IsTransitioning = false;
        }


        IEnumerator IUITransitionable.TransitionInRoutine()
        {
            IsTransitioning = true;

            gameObject.SetActive(true);

            Refresh();

            IsTransitioning = false;

            yield break;
        }

        IEnumerator IUITransitionable.TransitionOutRoutine()
        {
            IsTransitioning = true;

            gameObject.SetActive(false);

            IsTransitioning = false;

            yield break;
        }


        public static void Open()
        {
            IUITransitionable.StartTransitionInRoutine(SingletonInstance);
        }

        public static void Close()
        {
            IUITransitionable.StartTransitionOutRoutine(SingletonInstance);
        }

        public static IEnumerator OpenAndWait()
        {
            yield return IUITransitionable.StartTransitionInAndWait(SingletonInstance);
        }

        public static IEnumerator CloseAndWait()
        {
            yield return IUITransitionable.StartTransitionOutAndWait(SingletonInstance);
        }



        public bool IsTransitioning { get; set; } = false;

        public RoutinePlayer TransitionRoutine { get; } = new();
    }



    /* Example of IUITransitionable clonable */

    public class IUITransitionableClonableExample : UIClonable<IUITransitionableClonableExample>, IUITransitionable
    {
        public override void OnDisable()
        {
            base.OnDisable();

            IsTransitioning = false;
        }


        IEnumerator IUITransitionable.TransitionInRoutine()
        {
            IsTransitioning = true;

            gameObject.SetActive(true);

            Refresh();

            IsTransitioning = false;

            yield break;
        }

        IEnumerator IUITransitionable.TransitionOutRoutine()
        {
            IsTransitioning = true;

            gameObject.SetActive(false);

            IsTransitioning = false;

            Destroy(gameObject);

            yield break;
        }


        public static IUITransitionableClonableExample Open()
        {
            var clone = Clone();

            IUITransitionable.StartTransitionInRoutine(clone);

            return clone;
        }

        public void Close()
        {
            IUITransitionable.StartTransitionOutRoutine(this);
        }



        public bool IsTransitioning { get; set; } = false;

        public RoutinePlayer TransitionRoutine { get; } = new();
    }
}