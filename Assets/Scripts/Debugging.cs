using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Debugging : MonoBehaviour
{
    Text text;
    private bool gameOn;
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;

    // Use this for initialization
    void Start()
    {
        gameOn = true;
        text = GetComponent<Text>();
        //transform.position = new Vector3(distance.x, transform.position.y, transform.position.z);
    }


    void Update()
    {
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }
    }
    void FixedUpdate()
    {


        text.text = (Input.gyro.enabled) + "\n" +(Input.gyro.gravity) + "\n" + transform.rotation.eulerAngles;
        text.text += "\n" + m_lastFramerate;

    }
    // Update is called once per frame
}