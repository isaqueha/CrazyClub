using System.Collections.Generic;
using UnityEngine;

public class SystemOfParticles : MonoBehaviour {
    private List<Particle> particles = new List<Particle>();
    private PrimitiveType primitiveType = PrimitiveType.Sphere;
    private Birth birthType = Birth.Range;

    void Start () {}
	
	void Update () {
        changeTypesIfKeyPressed();
        createParticle();
        moveParticles();
    }

    private void changeTypesIfKeyPressed() {
        if (Input.GetKey(KeyCode.A)) {
            primitiveType = PrimitiveType.Cylinder;
        } else if (Input.GetKey(KeyCode.S)) {
            primitiveType = PrimitiveType.Cube;
        } else if (Input.GetKey(KeyCode.D)) {
            primitiveType = PrimitiveType.Sphere;
        } else if (Input.GetKey(KeyCode.W)) {
            birthType = Birth.Point;
        } else if (Input.GetKey(KeyCode.X)) {
            birthType = Birth.Range;
        }
    }

    private void createParticle() {
        var parameters = new ParticleParameters(primitiveType, birthType);
        particles.Add(new Particle(parameters));
    }

    private void moveParticles() {
        for (int position = particles.Count - 1; position >= 0; position--) {
            evolveParticle(position);
        }
    }

    private void evolveParticle(int position) {
        var particle = particles[position];
        if (particle.isAlive()) {
            particle.evolve();
        } else {
            destroyParticle(particle);
            removeParticleFromList(position);
        }
    }

    private void destroyParticle(Particle particle) {
        Destroy(particle.getGameObject());
    }

    private void removeParticleFromList(int position) {
        particles.RemoveAt(position);
    }
}
