using UnityEngine;

public class AudioLoudnessDetector : MonoBehaviour
{

    private int sampleWindow = 64;
    private AudioClip microphoneClip;
    private string microphoneName;
    void Start()
    {
        MicrophoneToAudioClip();
    }

    void OnApplicationQuit()
    {
        if (Microphone.IsRecording(microphoneName))
        {
            Microphone.End(microphoneName);
        }
    }

    private void Update()
    {
        if (Microphone.IsRecording(microphoneName))
        {
            GetLoudnessFromMicrophone();
        }
    }

    public float GetLoudnessFromMicrophone()
    {
        if (Microphone.devices.Length > 0)
        {
            float microLoudness = GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
            if (microLoudness > 0.005f) { Debug.Log(microLoudness); }
            return microLoudness;
        }
        else return -1;
    }

    public void MicrophoneToAudioClip()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneName = Microphone.devices[0];
            microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
        }
        else { Debug.Log("No microphone detected"); }
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0) return 0;
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / sampleWindow;
    }
}
