using System;
using System.Reflection;



namespace Rune
{
    public interface IGameEventData
    {
        
    }



    [AttributeUsage(AttributeTargets.Class)]
    public class GameEventAttribute : Attribute
    {
        public Type EventType { get; }

        public GameEventAttribute(Type eventType)
        {
            EventType = eventType;
        }
    }



    public static class GameEventFactory
    {
        public static GameEvent Create(IGameEventData data)
        {
            Type dataType = data.GetType();
    
            var attr = dataType.GetCustomAttribute<GameEventAttribute>() ?? throw new Exception($"GameEventAttribute not found on {dataType.Name}");

            Type eventType = attr.EventType;
    
            ConstructorInfo ctor = eventType.GetConstructor(new Type[] { dataType }) ?? throw new Exception($"{eventType.Name} does not have a constructor({dataType.Name})");
            
            return (GameEvent)ctor.Invoke(new object[] { data });
        }
    }
}