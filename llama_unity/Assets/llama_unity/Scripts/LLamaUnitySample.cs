using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using llama.unity;

public class LLamaUnitySample : MonoBehaviour
{
    // UI
    [SerializeField] private InputField _requestInput;
    [SerializeField] private Button _llmCompleteBtn;
    [SerializeField] private Text _llmResponseText;
    [SerializeField] private string llamaGGUFModel = "tinyllama-1.1b-chat-v1.0.Q8_0.gguf";

    private LLamaUnity llamaUnity;

    private WaitForSeconds wait = new WaitForSeconds(1f);


    private void Awake()
    {
        llamaUnity = new LLamaUnity();
        string llamaggufPath = Path.Combine(Application.streamingAssetsPath, llamaGGUFModel);
        llamaUnity.LoadLLMModel(llamaggufPath);
    }

    private void Start()
    {
        _llmCompleteBtn.onClick.AddListener(ClickListener);
    }

    private void OnDisable()
    {
        llamaUnity.Clear();
    }

    private void OnDestroy()
    {
        llamaUnity.Clear();
    }

    private void ClickListener()
    {
        string request = _requestInput.text;
        llamaUnity.RequestToLLM(request);

        _requestInput.text = string.Empty;
        _llmResponseText.text = string.Empty;

        StartCoroutine(ShowLlamaResponse());
    }

    private IEnumerator ShowLlamaResponse()
    {
        while (!llamaUnity.IsResponseEND)
        {
            _llmResponseText.text = llamaUnity.LLMResponse;

            yield return wait;
        }
    }
}
