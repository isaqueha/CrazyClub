using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpectrumData : MonoBehaviour
{
    Vector3[] originalVertices;
    Mesh gameObjectMesh;

    private void Start() {
        gameObjectMesh = gameObject.GetComponent<MeshFilter>().mesh;
        originalVertices = gameObjectMesh.vertices;        
    }

    void Update() {
        var spectrum = getSpectrumFromAudio();
        var newVertices = deformOriginalVertices(spectrum);
        gameObjectMesh.vertices = newVertices;
    }

    float[] getSpectrumFromAudio() {
        var spectrum = new float[64];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        return spectrum;
    }

    Vector3[] deformOriginalVertices(float[] spectrum) {
        var deformedVertices = new Vector3[originalVertices.Length];
        var counter = 0;
        for (int i = 0; i < originalVertices.Length; i++) {
            counter++;
            float x = originalVertices[i].x;
            float y = originalVertices[i].y;
            float z = originalVertices[i].z;
            if (counter == 20) {
                x = x * spectrum[0] * 10;
                counter = 0;
            }
            deformedVertices[i] = new Vector3(x, y, z);
        }
        return deformedVertices;
    }
}