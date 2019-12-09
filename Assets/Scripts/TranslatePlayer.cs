using UnityEngine;
using System.Collections;

public class TranslatePlayer : MonoBehaviour
{

    // Use this for initialization
    public Vector3 orientation;
    public float jetSpeed;
    public float strafeScale;
    public float multiplierSpeed;

    //private CardboardHead viewPort;


    void Start()
    {
        jetSpeed = -1.5f;
        //viewPort = GetComponentInChildren<CardboardHead>();
        orientation.y = 0;
    }


    void FixedUpdate()
    {
        // RotationCalculator();
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            if (transform.localEulerAngles.x >= 356.0f || transform.localEulerAngles.x == 0)
            {
                transform.Rotate(Vector3.right, Time.deltaTime * jetSpeed, Space.World);
            }
            else
            {
                transform.eulerAngles = new Vector3(359f, 0f, 0f);
            }
        }
    }

    public void RotationCalculator()
    {
        Vector3 pos = transform.position;
        pos.z = Vector3.Dot(Input.gyro.gravity*180, Vector3.right); //small sides -> bottom down and top up is + (this is the important one)
        pos.y = Vector3.Dot(Input.gyro.gravity*180, Vector3.up); // long sides -> bottom left and top right is +
        pos.x = Vector3.Dot(Input.gyro.gravity*180, Vector3.forward); // faces -> face down back up is +
        orientation.z = pos.z;
        orientation.x = pos.x;

        //if (orientation.z >= 180)
        //{
        //    angularDirection = orientation.z - 360.0f;
        //}
        //else
        //{
        //    angularDirection = orientation.z;
        //};
    }

}