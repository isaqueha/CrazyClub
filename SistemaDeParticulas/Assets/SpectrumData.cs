using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpectrumData : MonoBehaviour
{
    void Update() {
        var spectrum = getSpectrumFromAudio();
        changeGameObjectToNewPosition(spectrum);
    }

    float[] getSpectrumFromAudio() {
        var spectrum = new float[64];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        return spectrum;
    }

    void changeGameObjectToNewPosition(float[] spectrum) {
        var newPosition = new Vector3(spectrum[0] * 50, spectrum[32] * 50, spectrum[63] * 50);
        gameObject.transform.position = newPosition;
    }
}