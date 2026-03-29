using System;
using System.Collections.Generic;



namespace Rune
{
    [Obsolete("Use LooseEvent instead.")]
    public class SubscriberBase
    {
        public string name;
    }



    [Obsolete("Use LooseEvent instead.")]
    public class Subscriber : SubscriberBase
    {
        public Action action;
    }



    [Obsolete("Use LooseEvent instead.")]
    public class Subscriber<T0> : SubscriberBase
    {
        public Action<T0> action;
    }



    [Obsolete("Use LooseEvent instead.")]
    public class Subscriber<T0, T1> : SubscriberBase
    {
        public Action<T0, T1> action;
    }



    [Obsolete("Use LooseEvent instead.")]
    public class Subscriber<T0, T1, T2> : SubscriberBase
    {
        public Action<T0, T1, T2> action;
    }



    [Obsolete("Use LooseEvent instead.")]
    public abstract class PublisherBase
    {

    }



    [Obsolete("Use LooseEvent instead.")]
    public class Publisher : PublisherBase
    {
        public void Subscribe(string name, Action action)
        {
            _addeds.Add(new Subscriber { name = name, action = action });
        }

        public void Unsubscribe(string name)
        {
            _removeds.Add(name);
        }

        public void UnsubscribeAll()
        {
            foreach (var name in _subscribers.Keys) _removeds.Add(name);
        }


        public void SubscribeOnce(string name, Action action)
        {
            _addedsOnce.Add(new Subscriber { name = name, action = action });
        }

        public void UnsubscribeOnce(string name)
        {
            _removedsOnce.Add(name);
        }

        public void UnsubscribeOnceAll()
        {
            foreach (var name in _subscribersOnce.Keys) _removedsOnce.Add(name);
        }


        public void Clear()
        {
            _subscribers.Clear();
            _subscribersOnce.Clear();

            _addeds.Clear();
            _addedsOnce.Clear();

            _removeds.Clear();
            _removedsOnce.Clear();
        }

        public void Invoke()
        {
            foreach (var removed in _removeds) _subscribers.Remove(removed); _removeds.Clear();

            foreach (var added in _addeds) { if (_subscribers.ContainsKey(added.name)) _subscribers.Remove(added.name); _subscribers.Add(added.name, added.action); } _addeds.Clear();

            foreach (var subscriber in _subscribers.Values) subscriber();


            foreach (var removedDisposable in _removedsOnce) _subscribersOnce.Remove(removedDisposable); _removedsOnce.Clear();

            foreach (var addedDisposable in _addedsOnce) { if (_subscribersOnce.ContainsKey(addedDisposable.name)) _subscribersOnce.Remove(addedDisposable.name); _subscribersOnce.Add(addedDisposable.name, addedDisposable.action); } _addedsOnce.Clear();

            foreach (var subscriberDisposable in _subscribersOnce.Values) subscriberDisposable();


            _subscribersOnce.Clear();
        }



        private readonly Dictionary<string, Action> _subscribers = new();
        private readonly Dictionary<string, Action> _subscribersOnce = new();

        private readonly List<Subscriber> _addeds = new();
        private readonly List<Subscriber> _addedsOnce = new();

        private readonly List<string> _removeds = new();
        private readonly List<string> _removedsOnce = new();
    }



    [Obsolete("Use LooseEvent instead.")]
    public class Publisher<T> : PublisherBase
    {
        public void Subscribe(string name, Action<T> action)
        {
            _addeds.Add(new Subscriber<T> { name = name, action = action });
        }

        public void Unsubscribe(string name)
        {
            _removeds.Add(name);
        }

        public void UnsubscribeAll()
        {
            foreach (var name in _subscribers.Keys) _removeds.Add(name);
        }


        public void SubscribeOnce(string name, Action<T> action)
        {
            _addedsOnce.Add(new Subscriber<T> { name = name, action = action });
        }

        public void UnsubscribeOnce(string name)
        {
            _removedsOnce.Add(name);
        }

        public void UnsubscribeOnceAll()
        {
            foreach (var name in _subscribersOnce.Keys) _removedsOnce.Add(name);
        }


        public void Clear()
        {
            _subscribers.Clear();
            _subscribersOnce.Clear();

            _addeds.Clear();
            _addedsOnce.Clear();

            _removeds.Clear();
            _removedsOnce.Clear();
        }

        public void Invoke(T t)
        {
            foreach (var removed in _removeds) _subscribers.Remove(removed); _removeds.Clear();

            foreach (var added in _addeds) { if (_subscribers.ContainsKey(added.name)) _subscribers.Remove(added.name); _subscribers.Add(added.name, added.action); } _addeds.Clear();

            foreach (var subscriber in _subscribers.Values) subscriber(t);


            foreach (var removedDisposable in _removedsOnce) _subscribersOnce.Remove(removedDisposable); _removedsOnce.Clear();

            foreach (var addedDisposable in _addedsOnce) { if (_subscribersOnce.ContainsKey(addedDisposable.name)) _subscribersOnce.Remove(addedDisposable.name); _subscribersOnce.Add(addedDisposable.name, addedDisposable.action); } _addedsOnce.Clear();

            foreach (var subscriberDisposable in _subscribersOnce.Values) subscriberDisposable(t);


            _subscribersOnce.Clear();
        }



        private readonly Dictionary<string, Action<T>> _subscribers = new();
        private readonly Dictionary<string, Action<T>> _subscribersOnce = new();

        private readonly List<Subscriber<T>> _addeds = new();
        private readonly List<Subscriber<T>> _addedsOnce = new();

        private readonly List<string> _removeds = new();
        private readonly List<string> _removedsOnce = new();
    }



    [Obsolete("Use LooseEvent instead.")]
    public class Publisher<T0, T1> : PublisherBase
    {
        public void Subscribe(string name, Action<T0, T1> action)
        {
            _addeds.Add(new Subscriber<T0, T1> { name = name, action = action });
        }

        public void Unsubscribe(string name)
        {
            _removeds.Add(name);
        }

        public void UnsubscribeAll()
        {
            foreach (var name in _subscribers.Keys) _removeds.Add(name);
        }


        public void SubscribeOnce(string name, Action<T0, T1> action)
        {
            _addedsOnce.Add(new Subscriber<T0, T1> { name = name, action = action });
        }

        public void UnsubscribeOnce(string name)
        {
            _removedsOnce.Add(name);
        }

        public void UnsubscribeOnceAll()
        {
            foreach (var name in _subscribersOnce.Keys) _removedsOnce.Add(name);
        }


        public void Clear()
        {
            _subscribers.Clear();
            _subscribersOnce.Clear();

            _addeds.Clear();
            _addedsOnce.Clear();

            _removeds.Clear();
            _removedsOnce.Clear();
        }

        public void Invoke(T0 t0, T1 t1)
        {
            foreach (var removed in _removeds) _subscribers.Remove(removed); _removeds.Clear();

            foreach (var added in _addeds) { if (_subscribers.ContainsKey(added.name)) _subscribers.Remove(added.name); _subscribers.Add(added.name, added.action); } _addeds.Clear();

            foreach (var subscriber in _subscribers.Values) subscriber(t0, t1);


            foreach (var removedDisposable in _removedsOnce) _subscribersOnce.Remove(removedDisposable); _removedsOnce.Clear();

            foreach (var addedDisposable in _addedsOnce) { if (_subscribersOnce.ContainsKey(addedDisposable.name)) _subscribersOnce.Remove(addedDisposable.name); _subscribersOnce.Add(addedDisposable.name, addedDisposable.action); } _addedsOnce.Clear();

            foreach (var subscriberDisposable in _subscribersOnce.Values) subscriberDisposable(t0, t1);


            _subscribersOnce.Clear();
        }



        private readonly Dictionary<string, Action<T0, T1>> _subscribers = new();
        private readonly Dictionary<string, Action<T0, T1>> _subscribersOnce = new();

        private readonly List<Subscriber<T0, T1>> _addeds = new();
        private readonly List<Subscriber<T0, T1>> _addedsOnce = new();

        private readonly List<string> _removeds = new();
        private readonly List<string> _removedsOnce = new();
    }



    [Obsolete("Use LooseEvent instead.")]
    public class Publisher<T0, T1, T2> : PublisherBase
    {
        public void Subscribe(string name, Action<T0, T1, T2> action)
        {
            _addeds.Add(new Subscriber<T0, T1, T2> { name = name, action = action });
        }

        public void Unsubscribe(string name)
        {
            _removeds.Add(name);
        }

        public void UnsubscribeAll()
        {
            foreach (var name in _subscribers.Keys) _removeds.Add(name);
        }


        public void SubscribeOnce(string name, Action<T0, T1, T2> action)
        {
            _addedsOnce.Add(new Subscriber<T0, T1, T2> { name = name, action = action });
        }

        public void UnsubscribeOnce(string name)
        {
            _removedsOnce.Add(name);
        }

        public void UnsubscribeOnceAll()
        {
            foreach (var name in _subscribersOnce.Keys) _removedsOnce.Add(name);
        }


        public void Clear()
        {
            _subscribers.Clear();
            _subscribersOnce.Clear();

            _addeds.Clear();
            _addedsOnce.Clear();

            _removeds.Clear();
            _removedsOnce.Clear();
        }

        public void Invoke(T0 t0, T1 t1, T2 t2)
        {
            foreach (var removed in _removeds) _subscribers.Remove(removed); _removeds.Clear();

            foreach (var added in _addeds) { if (_subscribers.ContainsKey(added.name)) _subscribers.Remove(added.name); _subscribers.Add(added.name, added.action); } _addeds.Clear();

            foreach (var subscriber in _subscribers.Values) subscriber(t0, t1, t2);


            foreach (var removedDisposable in _removedsOnce) _subscribersOnce.Remove(removedDisposable); _removedsOnce.Clear();

            foreach (var addedDisposable in _addedsOnce) { if (_subscribersOnce.ContainsKey(addedDisposable.name)) _subscribersOnce.Remove(addedDisposable.name); _subscribersOnce.Add(addedDisposable.name, addedDisposable.action); } _addedsOnce.Clear();

            foreach (var subscriberDisposable in _subscribersOnce.Values) subscriberDisposable(t0, t1, t2);


            _subscribersOnce.Clear();
        }



        private readonly Dictionary<string, Action<T0, T1, T2>> _subscribers = new();
        private readonly Dictionary<string, Action<T0, T1, T2>> _subscribersOnce = new();

        private readonly List<Subscriber<T0, T1, T2>> _addeds = new();
        private readonly List<Subscriber<T0, T1, T2>> _addedsOnce = new();

        private readonly List<string> _removeds = new();
        private readonly List<string> _removedsOnce = new();
    }
}