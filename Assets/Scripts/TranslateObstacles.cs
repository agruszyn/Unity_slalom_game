using UnityEngine;
using System.Collections;

public class TranslateObstacles : MonoBehaviour
{

    // Use this for initialization
    public Vector3 orientation;
    public float jetSpeed;
    public float strafeScale;
    public float multiplierSpeed;

    //private CardboardHead viewPort;
    private float retainSpeed;


    void Start()
    {
        jetSpeed = -1.5f;
        //viewPort = GetComponentInChildren<CardboardHead>();
        orientation.y = 0;
    }


    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            transform.Rotate(Vector3.right, Time.deltaTime * jetSpeed, Space.World);
        }
    }

    public void RotationCalculator()
    {
    Vector3 pos = transform.position;
    pos.z = Vector3.Dot(Input.gyro.gravity*90, Vector3.left); //small sides -> bottom down and top up is + (this is the important one)
    pos.y = Vector3.Dot(Input.gyro.gravity*90, Vector3.down); // long sides -> bottom left and top right is +
    pos.x = Vector3.Dot(Input.gyro.gravity*90, Vector3.back); // faces -> face down back up is +
    orientation.z = pos.z;
    orientation.x = pos.x;
    }

}