# llama.unity
![llama_unity_upm](./Images/llama_unity_logo2.png)

llama.unity is a unity3d plugin to run llm model on your local device.

Based on [llama.cpp](https://github.com/ggerganov/llama.cpp).

it's convenient to use LLMs in your Unity Project

Supported platforms:

- [x] Mac OS (ARM)

## Quick start
### Use UPM(Unity Package Manager)
1. In Unity, Window > PackageManager. Click "Add Package from git URL" and put on :
```
https://github.com/DevCoop-code/llama.unity.git#upm
```
![llama_unity_upm](./Images/llama_unity_upm.png)

2. Download Models from [hugging face](https://huggingface.co/gguf)
- [Llama-2-7B-GGUF](https://huggingface.co/TheBloke/Llama-2-7B-GGUF/tree/main)
- [Llama-2-7B-Chat-GGUF](https://huggingface.co/TheBloke/Llama-2-7B-Chat-GGUF)

3. Make StreamingAssets Folder and put gguf model

![llama_unity_streamingasset](./Images/llama_unity_streamingassets.png)

4. Please Refer [LLamaUnitySample](./llama_unity/Assets/llama_unity/Runtime/Scripts/LLamaUnitySample.cs) Script

5. Please note that you need to add an extension as well
![need-extension](./Images/llama_unity_note_extension.png)

## Demo
<table class="center">
    <tr style="line-height: 0">
    <td width="50%" height=30 style="border: none; text-align: center">llama-2-7b-chat</td>
    </tr>
    <tr>
    <td width="50%" style="border: none;"><img src="Images/llama_unity_test.png" style="width:100%"></td>
    </tr>
</table>