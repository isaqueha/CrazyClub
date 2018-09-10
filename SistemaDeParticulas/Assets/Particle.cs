using UnityEngine;

public class Particle {
    private GameObject shape;
    private Vector3 position;
    private float timeToLive;
    
    public Particle(ParticleParameters parameters) {
        createGameObject(parameters.type);
        setInitialPosition(parameters.birth);
        setRandomTimeToLive();
        setRandomTrajectory();
    }

    public void evolve() {
        decreaseTimeToLive();
        changeColor();
    }

    public bool isAlive() {
        return hasTimeToLive();
    }

    public GameObject getGameObject() {
        return shape;
    }

    private bool hasTimeToLive() {
        return timeToLive >= 0;
    }

    private void createGameObject(PrimitiveType type) {
        shape = GameObject.CreatePrimitive(type);
        shape.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    private void setInitialPosition(Birth birth) {
        if (birth == Birth.Point) {
            shape.transform.position = new Vector3(0, 5, 0);
        } else {
            float x = Random.Range(-10, 10);
            shape.transform.position = new Vector3(x, 5, 0);
        }
    }

    private void setRandomTrajectory() {
        float value = Random.value;
        if (value < 0.33) {
            shape.AddComponent<LineBehaviour>();
        } else if (value < 0.67) {
            shape.AddComponent<BezierCurveBehaviour>();
        } else {
            shape.AddComponent<CatmullCurveBehaviour>();
        }
    }

    private void setRandomTimeToLive() {
        float random = Random.Range(156, 256);
        timeToLive = random / 256;
    }

    private void changeColor() {
        shape.GetComponent<Renderer>().material.color = new Color(1 - timeToLive, 0, timeToLive);
    }

    private void decreaseTimeToLive() {
        timeToLive -= 0.006f;
    }
}
