using System.Diagnostics;

namespace Core
{
    public static class Logger
    {
        public enum LogType : byte
        {
            Default = 0,
            Warning = 1,
            Error = 2,
            Critical = 3,
        }

        [Conditional("ENABLE_LOGS")]
        public static void Debug(string logMsg, LogType type = LogType.Default)
        {
            switch (type)
            {
                case LogType.Default:
                    UnityEngine.Debug.Log(logMsg);
                    break;
                case LogType.Warning:
                    UnityEngine.Debug.LogWarning(logMsg);
                    break;
                case LogType.Error:
                    UnityEngine.Debug.LogError(logMsg);
                    break;
                case LogType.Critical:
                    UnityEngine.Debug.LogAssertion(logMsg);
                    break;
            }
        }
    }
}
