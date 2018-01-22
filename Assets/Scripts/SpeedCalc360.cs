using UnityEngine;
using System.Collections;

public class SpeedCalc360 : MonoBehaviour {

	float rawSpeed, speedLimiter;
    float threshold;
    float calcSpeed, delta, degree;
    float angle, interval, classifier;
    int stopTime, steps;
    bool isMoving, isUp;
    public WifiConnector distanceGetter;
    public changeTexture speedSetter;

    // Use this for initialization
    void Start()
    {
        isMoving = false;
        rawSpeed = 0f;
        interval = 0f;
        threshold = 0.96f;
        calcSpeed = 0f;
        classifier = 0f;
        degree = 0f;
        stopTime = 0;
        steps = 0;
        speedLimiter = 12.0f;
        isUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        angle = distanceGetter.getDistance();
        //degree = distanceGetter.getAngle();
        if (angle == 0)
            angle = 1;
        if (angle <= threshold)
        {
            isMoving = true;
            interval = 0f;
            calcSpeed += 0.5f;
        }
        else
        {
            calcSpeed -= 0.001f;
        }

        if (angle <= threshold && !isUp)
        {
            steps++;
            isUp = true;
        }
        else if (angle > threshold)
            isUp = false;

        interval += Time.deltaTime;
        classifier += Time.deltaTime;

        if (interval >= 0.2f)
            calcSpeed = 0f;
        if (calcSpeed >= speedLimiter)
            calcSpeed = speedLimiter;

        //Debug.Log(calcSpeed);
        speedSetter.setZSpeed(calcSpeed);

    }
}
