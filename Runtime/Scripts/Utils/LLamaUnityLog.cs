using UnityEngine;

namespace llama.unity
{
    public static class LLamaUnityLog
    {
        [System.Diagnostics.Conditional("LLAMA_UNITY_LOG")]
        public static void Log(object message)
        {
            Debug.Log(message);
        }

        [System.Diagnostics.Conditional("LLAMA_UNITY_LOG")]
        public static void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }

        [System.Diagnostics.Conditional("LLAMA_UNITY_LOG")]
        public static void LogError(object message)
        {
            Debug.LogError(message);
        }
    }
}