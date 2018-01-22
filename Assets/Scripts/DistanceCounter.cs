using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

public class DistanceCounter : MonoBehaviour {

    float distReached, speed; 
    public float velocity, acceleration, limitVel;
    float time, timeSplit, speedoTimeSplit;
    Vector3 lastPosition;
    GameObject timeText, distText, speedText;
    public int accelMode;

	// Use this for initialization
	void Start ()
    {
        time = 0f;
        timeSplit = 0f;
        speedoTimeSplit = 0f;
        distReached = 0f;
        speed = 0f;
        lastPosition = transform.position;
        speedText = GameObject.Find("Speedometer");
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
        timeSplit += Time.deltaTime;
        speedoTimeSplit += Time.deltaTime;

        if(accelMode == 0)
            constAcceleration();
        else if (accelMode == 1)
            constVelocity();
        else if(accelMode == 2)
            accelerateAndStop();

        speed = Vector3.Distance(transform.position, lastPosition) / Time.deltaTime;
        distReached += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (speedoTimeSplit >= 0.4f)
        {
            speedText.GetComponent<Text>().text = "Speed: " + speed.ToString();
            speedoTimeSplit = 0f;
        }

        if (transform.position.y <= -2.0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        changeMode();
        //Debug.Log(velocity);
    }

    private void changeMode()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            accelMode = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKey(KeyCode.F2))
        {
            accelMode = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKey(KeyCode.F3))
        {
            accelMode = 2;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void constAcceleration()
    {
        transform.Translate(Vector3.forward * (this.acceleration * this.time));
    }

    private void constVelocity()
    {
        transform.Translate(Vector3.forward * (this.velocity));
    }

    private void accelerateAndStop()
    {
        if (this.velocity < this.limitVel)
            this.velocity += this.acceleration * this.time;
        else
            this.velocity = this.limitVel;
        transform.Translate(Vector3.forward * (this.velocity));
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Time split: " + timeSplit);
        timeSplit = 0f;
    }

}
