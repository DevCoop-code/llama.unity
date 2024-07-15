using System;
using System.Runtime.InteropServices;
using AOT;

namespace llama.unity
{
    public class LLamaUnityMac
    {
        protected delegate void LLMLogListener(string log);
        protected delegate void LLMResultListener(string result);
        protected delegate void LLMResponseCompleteListener();
        protected delegate void LLMClearCompleteListener();

        [DllImport("osxLLamaunity")]
        private static extern IntPtr GetVersion();

        [DllImport("osxLLamaunity")]
        private static extern int InitLLM();

        [DllImport("osxLLamaunity")]
        private static extern int RegisterLogListener(LLMLogListener funcPtr);

        [DllImport("osxLLamaunity")]
        private static extern void LoadLLM();

        [DllImport("osxLLamaunity")]
        private static extern void LoadLLMWithPath(string modelPath);

        [DllImport("osxLLamaunity")]
        private static extern void LLMComplete(string request, LLMResultListener funcPtr, LLMResponseCompleteListener completeFuncPtr);

        [DllImport("osxLLamaunity")]
        private static extern void LLMClear(LLMClearCompleteListener clearFuncPtr);


        private static LLamaUnityMac instance;
        public static LLamaUnityMac Instance
        {
            get
            {
                if (instance == null) { instance = new LLamaUnityMac(); }

                return instance;
            }
        }

        private static bool isResponseEND = true;
        public static bool IsResponseEND { get => isResponseEND; }
        private static string responseMsg = string.Empty;
        public static string Reply { get => responseMsg; }

        private LLMLogListener llmLogListener;
        private LLMResultListener llmResultListener;
        private LLMResponseCompleteListener llmResponseCompleteListener;
        private LLMClearCompleteListener llmClearCompleteListener;


        public LLamaUnityMac()
        {
            llmLogListener = new LLMLogListener(LlamaLogListener);
            llmResultListener = new LLMResultListener(LlamaResultListener);
            llmResponseCompleteListener = new LLMResponseCompleteListener(LlamaResponseCompleteListener);
            llmClearCompleteListener = new LLMClearCompleteListener(LlamaClearCompleteListener);

            int initResult = InitLLM();
            LLamaUnityLog.Log("[LLamaUnityMac] Init Result : " + initResult);

            RegisterLogListener(llmLogListener);
        }

        public void LoadLLMModel(string llamaggufPath)
        {
            LoadLLMWithPath(llamaggufPath);
        }

        public void RequestToLLM(string text)
        {
            if (!isResponseEND)
            {
                LLamaUnityLog.LogWarning("[LLamaUnityMac] not finished before request");

                return;
            }

            LLMComplete(text, llmResultListener, llmResponseCompleteListener);

            isResponseEND = false;
        }

        public void Clear()
        {
            LLMClear(llmClearCompleteListener);
        }

        public string GetCurrentVersion()
        {
            return Marshal.PtrToStringAnsi(GetVersion());
        }


        [MonoPInvokeCallback(typeof(LLMLogListener))]
        static void LlamaLogListener(string str)
        {
            LLamaUnityLog.Log("[LLamaUnityMac] llama unity log : " + str);
        }

        [MonoPInvokeCallback(typeof(LLMResultListener))]
        static void LlamaResultListener(string str)
        {
            LLamaUnityLog.Log("[LLamaUnityMac] llama result : " + str);

            responseMsg += str;
        }

        [MonoPInvokeCallback(typeof(LLMResponseCompleteListener))]
        static void LlamaResponseCompleteListener()
        {
            LLamaUnityLog.Log("[LLamaUnityMac] llama response end");

            responseMsg = string.Empty;
            isResponseEND = true;
        }

        [MonoPInvokeCallback(typeof(LLMClearCompleteListener))]
        static void LlamaClearCompleteListener()
        {
            LLamaUnityLog.Log("[LLamaUnityMac] llama Clear end");

            responseMsg = string.Empty;
        }
    }
}