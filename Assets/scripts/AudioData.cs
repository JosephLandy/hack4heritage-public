using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioData 
{
    // Start is called before the first frame update
    [System.NonSerialized]
    public AudioClip audioClip;
    [HideInInspector]
    public float[] audioClipData;
    public int sampleCount;

    public AudioData()
    {

    }
    public void CompressAndStore()
    {
        sampleCount = audioClip.samples * audioClip.channels;
        audioClipData = new float[sampleCount];
        audioClip.GetData(audioClipData, 0);
        audioClipData = new float[1];//unload this giant list
    }
    public void DecompressAndLoad()
    {
        audioClip = AudioClip.Create("Microphone", sampleCount, 1, 41000, false);
        audioClip.SetData(audioClipData, 0);
        audioClipData = new float[1];//unload this giant list
    }

}
