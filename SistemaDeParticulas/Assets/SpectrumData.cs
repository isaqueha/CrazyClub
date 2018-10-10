using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpectrumData : MonoBehaviour
{
    Vector3[] originalVertices;
    Mesh gameObjectMesh;
    public int deformedVertices = 4;
    public int frequencyBand = 0;

    private void Start() {
        gameObjectMesh = gameObject.GetComponent<MeshFilter>().mesh;
        originalVertices = gameObjectMesh.vertices;        
    }

    void Update() {
        var spectrum = getSpectrumFromAudio();
        MeshDeform deform = gameObject.GetComponent<MeshDeform>();
        if (deform) {
            for (int i = 0; i < deformedVertices; i++) {
                int rand = (int) (Random.value * originalVertices.Length);
                Vector3 point = originalVertices[rand];
                deform.DeformPoint(point, spectrum[frequencyBand]);
            }
        }
    }

    float[] getSpectrumFromAudio() {
        var spectrum = new float[64];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        return spectrum;
    }

}