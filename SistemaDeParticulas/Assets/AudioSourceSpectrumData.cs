//This script allows you to toggle music to play and stop.
//Assign an AudioSource to a GameObject and attach an Audio Clip in the Audio Source. Attach this script to the GameObject.

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceSpectrumData : MonoBehaviour
{
    void Update()
    {
        float[] spectrum = new float[64];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        var newPosition = new Vector3(spectrum[0]*50, spectrum[32] * 50, spectrum[63] * 50);
        gameObject.transform.position = newPosition;
    }
}