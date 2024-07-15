using UnityEngine;

namespace llama.unity
{
    public class LLamaUnity
    {
        private LLamaUnityMac llamaMac;

        public string LLMResponse
        {
            get
            {
                return LLamaUnityMac.Reply;
            }
        }

        public bool IsResponseEND
        {
            get
            {
                return LLamaUnityMac.IsResponseEND;
            }
        }

        private string modelExtension = ".gguf";
        private bool isModelLoaded = false;

        public LLamaUnity()
        {
#if UNITY_EDITOR_OSX
            llamaMac = LLamaUnityMac.Instance;
#elif UNITY_STANDALONE_OSX
        llamaMac = LLamaUnityMac.Instance;
#else
        LLamaUnityLog.LogError(Application.platform + " is not supported");
#endif
        }

        public void LoadLLMModel(string llamaggufPath)
        {
            if (!System.IO.File.Exists(llamaggufPath))
            {
                LLamaUnityLog.LogError("[LLamaUnity] Model is not exists. please put the gguf model to streamingAssets directory");

                return;
            }

            string extension = System.IO.Path.GetExtension(llamaggufPath);
            if (!extension.Equals(modelExtension))
            {
                LLamaUnityLog.LogError("[LLamaUnity] Model is not exists. please put the gguf model to streamingAssets directory : " + extension);

                return;
            }

            if (llamaMac != null)
            {
                llamaMac.LoadLLMModel(llamaggufPath);

                isModelLoaded = true;
            }
            else { LLamaUnityLog.LogError("llama is null"); }
        }

        public void RequestToLLM(string text)
        {
            if (llamaMac != null && isModelLoaded) { llamaMac.RequestToLLM(text); }
        }

        public void Clear()
        {
            if (llamaMac != null) { llamaMac.Clear(); }
        }
    }
}