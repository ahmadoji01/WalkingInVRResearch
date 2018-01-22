using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class SpeedCalc : MonoBehaviour {

    float rawSpeed;
    public float speedLimiter = 50.0f;
    float threshold;
    float calcSpeed, delta, degree;
    float angle, interval, classifier;
    int stopTime, steps;
    bool isMoving, isUp;
    public WifiConnector distanceGetter;
    public CharMove speedSetter;
    public RigidbodyFirstPersonController controller;

	// Use this for initialization
	void Start ()
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
        isUp = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        angle = distanceGetter.getDistance();
        //degree = distanceGetter.getAngle();
        if (angle == 0)
            angle = 1;
        if (angle <= threshold)
        {
            isMoving = true;
            interval = 0f;
            calcSpeed += 0.05f;
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
        else if(angle > threshold)
            isUp = false;

        interval += Time.deltaTime;
        classifier += Time.deltaTime;

        if (interval >= 0.2f)
            calcSpeed = 0f;
        if (calcSpeed >= speedLimiter)
            calcSpeed = speedLimiter;

        //Debug.Log(calcSpeed);
        
        speedSetter.speed = calcSpeed;
        controller.movementSettings.CurrentTargetSpeed = calcSpeed;
        controller.setMovingSpeed(calcSpeed);
        //controller.movementSettings.setAngleRot(degree);

        /*
        rawSpeed = distanceGetter.getDistance();
        threshold = distanceGetter.getThreshold();
        if (threshold == 0)
            calcSpeed = 0;
        if (rawSpeed >= threshold)
        {
            calcSpeed += 0.005f;
            stopTime = 0;
        }
        else if (rawSpeed >= threshold - (threshold * (1f - 0.2f)) && rawSpeed <= threshold - (threshold * (1f - 0.95f)))
        {
            if (calcSpeed > 0f)
                calcSpeed -= 0.001f;
            else
                calcSpeed = 0f;
            stopTime = 0;
        }
        else if (rawSpeed <= threshold - (threshold * (1f - 0.05f)))
        {
            stopTime++;
            if (calcSpeed > 0f)
                calcSpeed -= 0.005f;
            else
                calcSpeed = 0f;
            if (stopTime >= 4)
            {
                calcSpeed = 0f;
                stopTime = 0;
            }
        }
        velSetter.velocity = calcSpeed;
        */

    }
}
