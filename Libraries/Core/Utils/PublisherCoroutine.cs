using System;
using System.Collections;
using System.Collections.Generic;



namespace Rune
{
    public delegate IEnumerator Co();

    public delegate IEnumerator Co<T>(T t);



    [Obsolete("Use LooseEvent instead.")]
    public abstract class SubscriberCoroutineBase
    {
        public string name;
    }



    [Obsolete("Use LooseEvent instead.")]
    public class SubscriberCoroutine : SubscriberCoroutineBase
    {
        public Co coroutine;
    }



    [Obsolete("Use LooseEvent instead.")]
    public class SubscriberCoroutine<T> : SubscriberCoroutineBase
    {
        public Co<T> coroutine;
    }



    [Obsolete("Use LooseEvent instead.")]
    public abstract class PublisherCoroutineBase
    {

    }



    [Obsolete("Use LooseEvent instead.")]
    public class PublisherCoroutine : PublisherCoroutineBase
    {
        public void Subscribe(string name, Co coroutine)
        {
            _addeds.Add(new() { name = name, coroutine = coroutine });
        }

        public void Unsubscribe(string name)
        {
            _removeds.Add(name);
        }

        public void UnsubscribeAll()
        {
            foreach (var name in _subscribers.Keys) _removeds.Add(name);
        }


        public void SubscribeOnce(string name, Co coroutine)
        {
            _addedsOnce.Add(new() { name = name, coroutine = coroutine });
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

        public IEnumerator Invoke()
        {
            foreach (var removed in _removeds) _subscribers.Remove(removed); _removeds.Clear();

            foreach (var added in _addeds) { if (_subscribers.ContainsKey(added.name)) _subscribers.Remove(added.name); _subscribers.Add(added.name, added.coroutine); } _addeds.Clear();

            foreach (var subscriber in _subscribers.Values) yield return subscriber();


            foreach (var removedDisposable in _removedsOnce) _subscribersOnce.Remove(removedDisposable); _removedsOnce.Clear();

            foreach (var addedDisposable in _addedsOnce) { if (_subscribersOnce.ContainsKey(addedDisposable.name)) _subscribersOnce.Remove(addedDisposable.name); _subscribersOnce.Add(addedDisposable.name, addedDisposable.coroutine); } _addedsOnce.Clear();

            foreach (var subscriberDisposable in _subscribersOnce.Values) yield return subscriberDisposable();


            _subscribersOnce.Clear();


            yield break;
        }



        private readonly Dictionary<string, Co> _subscribers = new();
        private readonly Dictionary<string, Co> _subscribersOnce = new();

        private readonly List<SubscriberCoroutine> _addeds = new();
        private readonly List<SubscriberCoroutine> _addedsOnce = new();

        private readonly List<string> _removeds = new();
        private readonly List<string> _removedsOnce = new();
    }



    [Obsolete("Use LooseEvent instead.")]
    public class PublisherCoroutine<T> : PublisherCoroutineBase
    {
        public void Subscribe(string name, Co<T> coroutine)
        {
            _addeds.Add(new() { name = name, coroutine = coroutine });
        }

        public void Unsubscribe(string name)
        {
            _removeds.Add(name);
        }

        public void UnsubscribeAll()
        {
            foreach (var name in _subscribers.Keys) _removeds.Add(name);
        }


        public void SubscribeOnce(string name, Co<T> coroutine)
        {
            _addedsOnce.Add(new() { name = name, coroutine = coroutine });
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

        public IEnumerator Invoke(T t)
        {
            foreach (var removed in _removeds) _subscribers.Remove(removed); _removeds.Clear();

            foreach (var added in _addeds) { if (_subscribers.ContainsKey(added.name)) _subscribers.Remove(added.name); _subscribers.Add(added.name, added.coroutine); } _addeds.Clear();

            foreach (var subscriber in _subscribers.Values) yield return subscriber(t);


            foreach (var removedDisposable in _removedsOnce) _subscribersOnce.Remove(removedDisposable); _removedsOnce.Clear();

            foreach (var addedDisposable in _addedsOnce) { if (_subscribersOnce.ContainsKey(addedDisposable.name)) _subscribersOnce.Remove(addedDisposable.name); _subscribersOnce.Add(addedDisposable.name, addedDisposable.coroutine); } _addedsOnce.Clear();

            foreach (var subscriberDisposable in _subscribersOnce.Values) yield return subscriberDisposable(t);


            _subscribersOnce.Clear();


            yield break;
        }



        private readonly Dictionary<string, Co<T>> _subscribers = new();
        private readonly Dictionary<string, Co<T>> _subscribersOnce = new();

        private readonly List<SubscriberCoroutine<T>> _addeds = new();
        private readonly List<SubscriberCoroutine<T>> _addedsOnce = new();

        private readonly List<string> _removeds = new();
        private readonly List<string> _removedsOnce = new();
    }
}