using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveBehaviour : MonoBehaviour {
    private List<Vector3> points = new List<Vector3>();
    private int pointsCounter = 0;
    private float offset = 0;
    private float step = 0.02f;

    void Start() {
        setInitialPointAsControlPoint();
        setRandomControlPoints();
    }

    void Update() {
        resetCountersIfNeeded();
        moveToNextPoint();
    }

    private Vector3 getCurrentPoint() {
        return transform.position;
    }

    private void setInitialPointAsControlPoint() {
        points.Add(transform.position);
        points.Add(transform.position);
    }

    private void setRandomControlPoints() {
        Vector3 point;
        for(int y = 3; y >= -5; y--) {
            var x = Random.Range(-10, 10);
            point = new Vector3(x, y, 0);
            points.Add(point);
        }
    }

    private void resetCountersIfNeeded() {
        if (pointsCounter < points.Count) {
            if (offset >= 1f) {
                increaseStep();
                pointsCounter += 1;
                offset = 0;
            }
        }
    }

    private void moveToNextPoint() {
        var nextPoint = calculateBezierPoint(
            offset,
            points[pointsCounter],
            points[getNextValidPosition(1)],
            points[getNextValidPosition(2)],
            points[getNextValidPosition(3)]
        );
        transform.LookAt(nextPoint);
        transform.position = nextPoint;
        offset += step;
    }

    private int getNextValidPosition(int sum) {
        var nextPosition = pointsCounter + sum;
        if (nextPosition > points.Count - 1) {
            return nextPosition % 4;
        }
        return nextPosition;
    }

    private Vector3 calculateBezierPoint(
        float t,
        Vector3 firstPoint,
        Vector3 secondPoint,
        Vector3 thirdPoint,
        Vector3 fourthPoint
        ) {
        var t3 = t * t * t;
        var t2 = t * t;
        return ((-1.0f * t3 + 3.0f * t2 - 3.0f * t + 1.0f) * firstPoint +
            (3.0f * t3 - 6.0f * t2 + 0.0f * t + 4.0f) * secondPoint +
            (-3.0f * t3 + 3.0f * t2 + 3.0f * t + 1.0f) * thirdPoint +
            (1.0f * t3 + 0.0f * t2 + 0.0f * t + 0.0f) * fourthPoint) / 6.0f;
    }
    
    private void increaseStep() {
        step += 0.01f;
    }
}
