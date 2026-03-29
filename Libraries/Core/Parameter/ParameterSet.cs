using System;
using System.Collections.Generic;



namespace Rune
{
    public class ParameterSet : ISaveable<ParameterSet.SaveData>
    {
        public void Clear()
        {
            _parameters.Clear();

            _flags.Clear();

            OnChangeParameter.Invoke();

            OnChangeFlag.Invoke();
        }
        

        public void AddParameter<T>(string name, T value, bool notify = true)
        {
            var newParameter = new Parameter<T>(name, value);

            _parameters[name] = newParameter;

            if (notify)
            {
                OnChangeParameter.Invoke();

                OnAddParameter.Invoke(new() { key = name, parameter = newParameter });
            }
        }

        public void RemoveParameter(string name, bool notify = true)
        {
            _parameters.Remove(name);

            if (notify)
            {
                OnChangeParameter.Invoke();

                OnRemoveParameter.Invoke(new() { key = name });
            }
        }

        public void ModifyParameter<T>(string name, T value, bool notify = true)
        {
            if (!_parameters.TryGetValue(name, out var parameter))
            {
                DebugManager.Log($"Parameter not found: ({name})");

                return;
            }

            if (parameter is not Parameter<T> parameterT)
            {
                DebugManager.Log($"Type mismatch for parameter (Name: {name}, Expected: {typeof(T).Name}, Parameter: {parameter.GetType()})");

                return;
            }

            parameterT.Value = value;

            if (notify) OnChangeParameter.Invoke();
        }

        public T GetParameter<T>(string name)
        {
            if (!_parameters.TryGetValue(name, out var parameter))
            {
                DebugManager.Log($"Parameter not found: ({name})");

                return default;
            }

            if (parameter is not Parameter<T> parameterT)
            {
                DebugManager.Log($"Type mismatch for parameter (Name: {name}, Expected: {typeof(T).Name}, Parameter: {parameter.GetType()})");

                return default;
            }

            return parameterT.Value;
        }

        public bool TryGetParameter<T>(string name, out T value)
        {
            value = default;

            if (!_parameters.TryGetValue(name, out var parameter)) return false;

            if (parameter is not Parameter<T> parameterT) return false;

            value = parameterT.Value;

            return true;
        }

        public bool HasParameter<T>(string name)
        {
            if (!_parameters.TryGetValue(name, out var parameter)) return false;

            if (parameter is not Parameter<T> parameterT) return false;

            return true;
        }

        public bool HasParameter<T>(string name, T value)
        {
            if (!_parameters.TryGetValue(name, out var parameter)) return false;

            if (parameter is not Parameter<T> parameterT) return false;

            return EqualityComparer<T>.Default.Equals(parameterT.Value, value);
        }


        public void AddFlag(string name, bool notify = true)
        {
            _flags.Add(name);

            if (notify)
            {
                OnChangeFlag.Invoke();

                OnAddFlag.Invoke(new() { key = name });
            }
        }

        public void RemoveFlag(string name, bool notify = true)
        {
            _flags.Remove(name);
            
            if (notify)
            {
                OnChangeFlag.Invoke();

                OnRemoveFlag.Invoke(new() { key = name });
            }
        }

        public bool HasFlag(string name)
        {
            return _flags.Contains(name);
        }


        public SaveData ExportSaveData()
        {
            var saveData = new SaveData();


            foreach (var parameter in _parameters.Values)
            {
                string name = parameter.Name;
                string type = Parameter.TypeToString(parameter.Type);

                switch (parameter.Type)
                {
                    case ParameterType.Int: saveData.parameters.Add(new() { name = name, type = type, value = (parameter as Parameter<int>).Value.ToString() }); break;
                    case ParameterType.Float: saveData.parameters.Add(new() { name = name, type = type, value = (parameter as Parameter<float>).Value.ToString() }); break;
                    case ParameterType.Bool: saveData.parameters.Add(new() { name = name, type = type, value = (parameter as Parameter<bool>).Value.ToString() }); break;
                    case ParameterType.String: saveData.parameters.Add(new() { name = name, type = type, value = (parameter as Parameter<string>).Value.ToString() }); break;
                }
            }


            foreach (var flag in _flags)
            {
                saveData.flags.Add(flag);
            }


            return saveData;
        }

        public void ImportSaveData(SaveData saveData)
        {
            if (saveData == null) return;


            _parameters.Clear();

            foreach (var parameter in saveData.parameters)
            {
                string name = parameter.name;
                string value = parameter.value;

                switch (Parameter.StringToType(parameter.type))
                {
                    case ParameterType.Int: _parameters[name] = new Parameter<int>(name, int.Parse(value)); break;
                    case ParameterType.Float: _parameters[name] = new Parameter<float>(name, float.Parse(value)); break;
                    case ParameterType.Bool: _parameters[name] = new Parameter<bool>(name, bool.Parse(value)); break;
                    case ParameterType.String: _parameters[name] = new Parameter<string>(name, value); break;
                };
            }


            _flags.Clear();

            foreach (var flag in saveData.flags)
            {
                _flags.Add(flag);
            }
        }



        public LooseEvent OnChangeParameter { get; } = new();
        public LooseEvent OnChangeFlag { get; } = new();

        public LooseEvent<ParameterAddInfo> OnAddParameter { get; } = new();
        public LooseEvent<ParameterRemoveInfo> OnRemoveParameter { get; } = new();
        public LooseEvent<FlagAddInfo> OnAddFlag { get; } = new();
        public LooseEvent<FlagRemoveInfo> OnRemoveFlag { get; } = new();



        private readonly Dictionary<string, Parameter> _parameters = new();

        private readonly HashSet<string> _flags = new();



        public enum ParameterType
        {
            Int,
            Float,
            Bool,
            String,
        }



        public abstract class Parameter
        {
            protected static ParameterType ResolveType(Type type)
            {
                if (type == typeof(int)) return ParameterType.Int;
                if (type == typeof(float)) return ParameterType.Float;
                if (type == typeof(bool)) return ParameterType.Bool;
                if (type == typeof(string)) return ParameterType.String;
                throw new NotSupportedException($"Unsupported parameter type. ({type})");
            }


            public static string TypeToString(ParameterType type)
            {
                if (type == ParameterType.Int) return "int";
                if (type == ParameterType.Float) return "float";
                if (type == ParameterType.Bool) return "bool";
                if (type == ParameterType.String) return "string";
                throw new NotSupportedException($"Unsupported parameter type. ({type})");
            }

            public static ParameterType StringToType(string str)
            {
                return str switch
                {
                    "int" => ParameterType.Int,
                    "float" => ParameterType.Float,
                    "bool" => ParameterType.Bool,
                    "string" => ParameterType.String,
                    _ => throw new NotSupportedException($"Unsupported parameter type. ({str})"),
                };
            }



            public abstract string ValueToString();



            public string Name { get; protected set; }
            
            public ParameterType Type { get; protected set; }
        }



        public class Parameter<T> : Parameter
        {
            public Parameter(string name, T value)
            {
                Name = name;

                Type = ResolveType(typeof(T));

                Value = value;
            }



            public override string ValueToString()
            {
                return Value.ToString();
            }



            public T Value { get; set; }
        }



        public class FlagAddInfo
        {
            public string key;
        }

        public class FlagRemoveInfo
        {
            public string key;
        }

        public class ParameterAddInfo
        {
            public string key;

            public Parameter parameter;
        }

        public class ParameterRemoveInfo
        {
            public string key;
        }



        [Serializable]
        public class SaveData
        {
            public List<ParameterData> parameters = new();

            public List<string> flags = new();
        


            [Serializable]
            public class ParameterData
            {
                public string name;
                public string type;
                public string value;
            }
        }
    }



    public static class ParameterSetExtensions
    {
        public static void AddInt(this ParameterSet set, string name, int value, bool notify = true)
        {
            set.AddParameter(name, value, notify);
        }

        public static void ModifyInt(this ParameterSet set, string name, int value, bool notify = true)
        {
            set.ModifyParameter(name, value, notify);
        }

        public static int GetInt(this ParameterSet set, string name)
        {
            return set.GetParameter<int>(name);
        }

        public static bool TryGetInt(this ParameterSet set, string name, out int value)
        {
            return set.TryGetParameter(name, out value);
        }

        public static bool HasInt(this ParameterSet set, string name)
        {
            return set.HasParameter<int>(name);
        }

        public static bool HasInt(this ParameterSet set, string name, int value)
        {
            return set.HasParameter(name, value);
        }

        public static void IncreaseInt(this ParameterSet set, string name, int value, bool notify = true)
        {
            if (!HasInt(set, name))
            {
                AddInt(set, name, 0, notify);
            }

            if (TryGetInt(set, name, out var p))
            {
                ModifyInt(set, name, p + value, notify);
            }
        }


        public static void AddFloat(this ParameterSet set, string name, float value, bool notify = true)
        {
            set.AddParameter(name, value, notify);
        }

        public static float GetFloat(this ParameterSet set, string name)
        {
            return set.GetParameter<float>(name);
        }

        public static bool TryGetFloat(this ParameterSet set, string name, out float value)
        {
            return set.TryGetParameter(name, out value);
        }

        public static bool HasFloat(this ParameterSet set, string name)
        {
            return set.HasParameter<float>(name);
        }
        
        public static bool HasFloat(this ParameterSet set, string name, float value)
        {
            return set.HasParameter(name, value);
        }


        public static void AddString(this ParameterSet set, string name, string value, bool notify = true)
        {
            set.AddParameter(name, value, notify);
        }

        public static string GetString(this ParameterSet set, string name)
        {
            return set.GetParameter<string>(name);
        }

        public static bool TryGetString(this ParameterSet set, string name, out string value)
        {
            return set.TryGetParameter(name, out value);
        }

        public static bool HasString(this ParameterSet set, string name)
        {
            return set.HasParameter<string>(name);
        }
        
        public static bool HasString(this ParameterSet set, string name, string value)
        {
            return set.HasParameter(name, value);
        }
    }
}