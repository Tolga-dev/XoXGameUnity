using UnityEngine;

namespace Core
{
    public class Debugger : Singleton<Debugger>
    {
        public static bool IsDebugMode = false;

        public static void Log(string debug)
        {
            if (IsDebugMode) return;
            
            Debug.Log(debug);
        }

        public static void LogWarning(string debug)
        {
            if (IsDebugMode) return;
            
            Debug.Log(debug);
        }
    }
}