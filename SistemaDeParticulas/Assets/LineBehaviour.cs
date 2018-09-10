using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehaviour : MonoBehaviour {
    private Vector3 finalPoint;
    const float step = 0.05f;

    void Start() {
        setRandomFinalPoint();
    }

    void Update () {
        if (!isOnFinalPoint()) {
            moveToNextPoint();
        }
    }

    public bool isOnFinalPoint() {
        var currentPoint = getCurrentPoint();
        return currentPoint.Equals(finalPoint);
    }

    private void setRandomFinalPoint() {
        float x = Random.Range(-10, 10);
        finalPoint = new Vector3(x, -3, 0);
    }

    private void moveToNextPoint() {
        var nextPoint = calculateNextPoint();
        transform.LookAt(nextPoint);
        transform.position = nextPoint;
    }

    private Vector3 calculateNextPoint() {
        var currentPoint = getCurrentPoint();
        var nextPoint = finalPoint - currentPoint;
        float magnitude = nextPoint.magnitude;
        if (magnitude <= step || magnitude == 0f) {
            return finalPoint;
        }
        return currentPoint + nextPoint / magnitude * step;
    }

    private Vector3 getCurrentPoint() {
        return transform.position;
    }
}
