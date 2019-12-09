using UnityEngine;
using System.Collections;

public class headtracking : MonoBehaviour {

    //private CardboardHead viewPort;
    private Vector3 orientation;
    private bool vrMode;

    // Use this for initialization
    void Start ()
    {
        orientation.y = 0;
        vrMode = PlayerPrefs.GetInt("VRmode") == 1;
        //viewPort = GetComponentInChildren<CardboardHead>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = Vector3.Dot(Input.gyro.gravity*90, Vector3.left); //small sides -> bottom down and top up is + (this is the important one)
        pos.y = Vector3.Dot(Input.gyro.gravity*90, Vector3.down); // long sides -> bottom left and top right is +
        pos.x = Vector3.Dot(Input.gyro.gravity*90, Vector3.back); // faces -> face down back up is +
        vrMode = PlayerPrefs.GetInt("VRmode") == 1;
        if (vrMode == true)      
        {
            if (pos.x > 315 || pos.x < 45)
        {
            orientation.x = pos.x;
        }
        if (pos.z > 315 || pos.z < 45)
        {
            orientation.z = pos.z;
        }
        transform.localEulerAngles = orientation;
    }
        else if (vrMode == false)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }

    }
}
