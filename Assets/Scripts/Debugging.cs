using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Debugging : MonoBehaviour
{
    Text text;
    private bool gameOn;

    // Use this for initialization
    void Start()
    {
        gameOn = true;
        text = GetComponent<Text>();
        //transform.position = new Vector3(distance.x, transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {


        text.text = (Input.gyro.enabled) + "\n" +(Input.gyro.gravity) + "\n" + transform.rotation.eulerAngles;

    }
    // Update is called once per frame
}