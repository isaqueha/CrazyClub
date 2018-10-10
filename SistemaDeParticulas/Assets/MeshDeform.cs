using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeform : MonoBehaviour {
    Mesh deformedMesh;
    Vector3[] originalVertices, displacedVertices;
    Vector3[] vertexForces;
    public float force = 1000f;
    public float forceOffset = 5f;
    public float springForce = 20f;
    public float damping = 5f;

    void Start() {
        deformedMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformedMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        vertexForces = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++) {
            displacedVertices[i] = originalVertices[i];
        }
    }

    public void DeformPoint(Vector3 point, float forceSpectrum) {
        int rand = (int)(Random.value * originalVertices.Length);
        point += deformedMesh.normals[rand] * forceOffset;
        forceSpectrum = forceSpectrum * force;
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            DeformVertexWithForce(i, point, forceSpectrum);
        }
    }

    void DeformVertexWithForce(int i, Vector3 point, float forceSpectrum) {
        Vector3 pointToVertex = displacedVertices[i] - point;
        float attenuatedForce = forceSpectrum / (1f + pointToVertex.sqrMagnitude);
        vertexForces[i] = pointToVertex.normalized * attenuatedForce;
    }

    void Update() {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            UpdateVertex(i);
        }
        deformedMesh.vertices = displacedVertices;
        deformedMesh.RecalculateNormals();
    }

    void UpdateVertex(int i) {
        Vector3 displaceForce = vertexForces[i];
        Vector3 displacement = displacedVertices[i] - originalVertices[i];
        displaceForce -= displacement * springForce * Time.deltaTime;
        displaceForce *= 1f - damping * Time.deltaTime;
        vertexForces[i] = displaceForce;
        displacedVertices[i] += displaceForce;
    }
}
