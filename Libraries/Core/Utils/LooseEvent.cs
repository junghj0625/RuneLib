using System;



namespace Rune
{
    public class LooseEventBase
    {

    }



    public class LooseEvent : LooseEventBase
    {
        public void AddListener(Action handler)
        {
            RemoveListener(handler);

            _handlers += handler;
        }

        public void AddOnceListener(Action handler)
        {
            Action wrapper = null;

            void Wrapper()
            {
                RemoveListener(wrapper);

                handler();
            }

            wrapper = Wrapper;

            AddListener(wrapper);
        }

        public void RemoveListener(Action handler)
        {
            _handlers -= handler;
        }

        public void RemoveAllListeners()
        {
            _handlers = null;
        }

        public void Invoke()
        {
            var handlerSnapshot = _handlers;

            handlerSnapshot?.Invoke();
        }


        public static LooseEvent operator +(LooseEvent e, Action h)
        {
            e.AddListener(h);

            return e;
        }

        public static LooseEvent operator -(LooseEvent e, Action h)
        {
            e.RemoveListener(h);

            return e;
        }



        private Action _handlers;
    }



    public class LooseEvent<T> : LooseEventBase
    {
        public void AddListener(Action<T> handler)
        {
            RemoveListener(handler);

            _handlers += handler;
        }

        public void AddOnceListener(Action<T> handler)
        {
            Action<T> wrapper = null;

            void Wrapper(T t)
            {
                RemoveListener(wrapper);

                handler(t);
            }

            wrapper = Wrapper;

            AddListener(wrapper);
        }

        public void RemoveListener(Action<T> handler)
        {
            _handlers -= handler;
        }

        public void RemoveAllListeners()
        {
            _handlers = null;
        }

        public void Invoke(T arg)
        {
            var handlerSnapshot = _handlers;

            handlerSnapshot?.Invoke(arg);
        }


        public static LooseEvent<T> operator +(LooseEvent<T> e, Action<T> h)
        {
            e.AddListener(h);

            return e;
        }

        public static LooseEvent<T> operator -(LooseEvent<T> e, Action<T> h)
        {
            e.RemoveListener(h);

            return e;
        }



        private Action<T> _handlers;
    }



    public class LooseEvent<T0, T1> : LooseEventBase
    {
        public void AddListener(Action<T0, T1> handler)
        {
            RemoveListener(handler);

            _handlers += handler;
        }

        public void AddOnceListener(Action<T0, T1> handler)
        {
            Action<T0, T1> wrapper = null;

            void Wrapper(T0 t0, T1 t1)
            {
                RemoveListener(wrapper);

                handler(t0, t1);
            }

            wrapper = Wrapper;

            AddListener(wrapper);
        }

        public void RemoveListener(Action<T0, T1> handler)
        {
            _handlers -= handler;
        }

        public void RemoveAllListeners()
        {
            _handlers = null;
        }

        public void Invoke(T0 arg0, T1 arg1)
        {
            var handlerSnapshot = _handlers;

            handlerSnapshot?.Invoke(arg0, arg1);
        }


        public static LooseEvent<T0, T1> operator +(LooseEvent<T0, T1> e, Action<T0, T1> h)
        {
            e.AddListener(h);

            return e;
        }

        public static LooseEvent<T0, T1> operator -(LooseEvent<T0, T1> e, Action<T0, T1> h)
        {
            e.RemoveListener(h);

            return e;
        }



        private Action<T0, T1> _handlers;
    }
    


    public class LooseEvent<T0, T1, T2> : LooseEventBase
    {
        public void AddListener(Action<T0, T1, T2> handler)
        {
            RemoveListener(handler);

            _handlers += handler;
        }

        public void AddOnceListener(Action<T0, T1, T2> handler)
        {
            Action<T0, T1, T2> wrapper = null;

            void Wrapper(T0 t0, T1 t1, T2 t2)
            {
                RemoveListener(wrapper);

                handler(t0, t1, t2);
            }

            wrapper = Wrapper;

            AddListener(wrapper);
        }

        public void RemoveListener(Action<T0, T1, T2> handler)
        {
            _handlers -= handler;
        }

        public void RemoveAllListeners()
        {
            _handlers = null;
        }

        public void Invoke(T0 arg0, T1 arg1, T2 arg2)
        {
            var handlerSnapshot = _handlers;

            handlerSnapshot?.Invoke(arg0, arg1, arg2);
        }

        public static LooseEvent<T0, T1, T2> operator +(LooseEvent<T0, T1, T2> e, Action<T0, T1, T2> h)
        {
            e.AddListener(h);

            return e;
        }

        public static LooseEvent<T0, T1, T2> operator -(LooseEvent<T0, T1, T2> e, Action<T0, T1, T2> h)
        {
            e.RemoveListener(h);

            return e;
        }



        private Action<T0, T1, T2> _handlers;
    }
}