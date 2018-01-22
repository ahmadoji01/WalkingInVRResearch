using UnityEngine;
using System.Collections;

public class changeTexture : MonoBehaviour {

    public Texture[] textures;
    public GameObject GVRCam;
    public float interval;
    private float zSpeed;
    private int numOfTex, texIndex;

    public void setZSpeed(float speed)
    {
        this.zSpeed = speed;
    }

	// Use this for initialization
	void Start ()
    {
        numOfTex = textures.Length;
        texIndex = 0;
        zSpeed = 0;
        GetComponent<Renderer>().material.mainTexture = textures[0];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            texIndex++;
            if (texIndex >= numOfTex)
                texIndex = 0;
            GetComponent<Renderer>().material.mainTexture = textures[texIndex];
        }

        GVRCam.transform.Translate(Vector3.forward * Time.deltaTime * zSpeed);
        //GVRCam.transform.Translate(Vector3.right * Time.deltaTime * xSpeed);

        Debug.Log(GVRCam.transform.position.z);
        if (GVRCam.transform.position.z >= interval)
        {
            texIndex++;
            if (texIndex >= numOfTex)
                texIndex = 0;
            GetComponent<Renderer>().material.mainTexture = textures[texIndex];
            GVRCam.transform.position = new Vector3(0.0f, 20.0f, 0.0f);
        } 
    }
}
