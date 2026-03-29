using System;
using System.Reflection;



namespace Rune
{
    public interface IConditionData
    {
        
    }



    [AttributeUsage(AttributeTargets.Class)]
    public class ConditionAttribute : Attribute
    {
        public Type EventType { get; }

        public ConditionAttribute(Type eventType)
        {
            EventType = eventType;
        }
    }



    public static class ConditionFactory
    {
        public static Condition Create(IConditionData data)
        {
            Type dataType = data.GetType();
    
            var attr = dataType.GetCustomAttribute<ConditionAttribute>() ?? throw new Exception($"ConditionAttribute not found on {dataType.Name}");

            Type eventType = attr.EventType;
    
            ConstructorInfo ctor = eventType.GetConstructor(new Type[] { dataType }) ?? throw new Exception($"{eventType.Name} does not have a constructor({dataType.Name})");
            
            return (Condition)ctor.Invoke(new object[] { data });
        }
    }
}