using System.Collections.Generic;



namespace Rune
{
    public class KeyedStack<TKey, TValue>
    {
        public void Push(TKey key, TValue value)
        {
            _stack.Add(new Entry(key, value));

            OnChangeCurrent.Invoke(Current);
        }

        public void Pop(TKey key)
        {
            for (int i = _stack.Count - 1; i >= 0; i--)
            {
                if (EqualityComparer<TKey>.Default.Equals(_stack[i].Key, key))
                {
                    _stack.RemoveAt(i);

                    break;
                }
            }

            OnChangeCurrent.Invoke(Current);
        }

        public void Pop()
        {
            if (_stack.Count > 0) _stack.RemoveAt(_stack.Count - 1);

            OnChangeCurrent.Invoke(Current);
        }

        public void Clear()
        {
            _stack.Clear();

            OnChangeCurrent.Invoke(Current);
        }

        public void Refresh()
        {
            OnChangeCurrent.Invoke(Current);
        }



        public TValue Current
        {
            get
            {
                if (_stack.Count == 0) return default;

                return _stack[^1].Value;
            }
        }



        public int Count => _stack.Count;

        public LooseEvent<TValue> OnChangeCurrent { get; } = new();



        private readonly List<Entry> _stack = new();



        private struct Entry
        {
            public Entry(TKey key, TValue value)
            {
                Key = key;

                Value = value;
            }



            public TKey Key;

            public TValue Value;
        }
    }
}