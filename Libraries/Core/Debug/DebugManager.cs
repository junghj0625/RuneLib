using UnityEngine;



namespace Rune
{
    public class DebugManager : MonoPlusSingleton<DebugManager>
    {
        public static void Log(object message, LogType type = LogType.Log)
        {
            var str = message != null ? message.ToString() : "null";

            OnLog.Invoke(str, type);

            #if UNITY_EDITOR
            switch (type)
            {
                case LogType.Log:
                    Debug.Log(str);
                    break;

                case LogType.Warning:
                    Debug.LogWarning(str);
                    break;

                case LogType.Error:
                    Debug.LogError(str);
                    break;
            }
            #endif
        }



        public static LooseEvent<string, LogType> OnLog { get; } = new();



        public enum LogType
        {
            Log,
            Warning,
            Error,
        }
    }
}