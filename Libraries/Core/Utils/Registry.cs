using System.Collections.Generic;



namespace Rune
{
    public abstract class RegistryBase
    {
        public abstract void Refresh();



        public LooseEvent<bool> OnChange { get; } = new();

        public abstract bool IsRegistered { get; }
    }



    public class Registry<T> : RegistryBase
    {
        public void Register(T key)
        {
            if (_users.Add(key))
            {
                OnChange.Invoke(IsRegistered);
            }
        }

        public void Unregister(T key)
        {
            if (_users.Remove(key))
            {
                OnChange.Invoke(IsRegistered);
            }
        }

        public bool Has(T key)
        {
            return _users.Contains(key);
        }


        public override void Refresh()
        {
            OnChange.Invoke(IsRegistered);
        }



        public override bool IsRegistered
        {
            get => _users.Count > 0;
        }



        private readonly HashSet<T> _users = new();
    }
}