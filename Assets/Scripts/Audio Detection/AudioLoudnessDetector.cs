using UnityEngine;

public class AudioLoudnessDetector : MonoBehaviour
{


    public static AudioLoudnessDetector instance;
    private int sampleWindow = 64;
    private AudioClip microphoneClip;
    private string microphoneName;

    void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }


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
            float microLoudness = 100 * GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
            if (microLoudness > 1) { Debug.Log(microLoudness); }
            if (microLoudness > 4) { Debug.Log("Very loud"); }
            return microLoudness;
        }
        else return -1;
    }

    private void MicrophoneToAudioClip()
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
