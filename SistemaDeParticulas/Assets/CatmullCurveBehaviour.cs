using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmullCurveBehaviour : MonoBehaviour {
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

    private void setInitialPointAsControlPoint() {
        points.Add(transform.position);
        points.Add(transform.position);
    }

    private void setRandomControlPoints() {
        Vector3 point;
        for (int y = 3; y >= -5; y--) {
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
        var nextPoint = calculateCatmullPoint(
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
    
    private Vector3 calculateCatmullPoint(
        float t,
        Vector3 firstPoint,
        Vector3 secondPoint,
        Vector3 thirdPoint,
        Vector3 fourthPoint) {
        return 0.5f * ((2 * secondPoint) + (-firstPoint + thirdPoint) * t +
            (2 * firstPoint - 5 * secondPoint + 4 * thirdPoint - fourthPoint) * t * t +
            (-firstPoint + 3 * secondPoint - 3 * thirdPoint + fourthPoint) * t * t * t);
    }

    private void increaseStep() {
        step += 0.01f;
    }
}
