using UnityEngine;
using System.Collections;

public class Game_Master : MonoBehaviour
{

    // Use this for initialization

    public float fUserOffsetT;
    public float fUserOffsetx;
    public float fUserOffsetz;
    const float tileWidth = 20.944f;
    const float degrees = 1.2f;
    public int quadrant;
    float fStartTime;
    private Score level;
    GameObject canv;

    public Material RED;
    public bool keydown;
    public Material BLUE;




    void Start()
    {
        canv = GameObject.Find("Score");
        if (canv != null)
        {
            level = canv.GetComponent<Score>();
        }
        RED = Resources.Load("RED") as Material;
        BLUE = Resources.Load("BLUE") as Material;
        PlayerPrefs.SetInt("VRmode", 0);
        PlayerPrefs.SetInt("pause", 0); ;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && PlayerPrefs.GetInt("pause") == 0 && level.score > 5)
        {
            PlayerPrefs.SetInt("pause", 1);
        }
        else if (Input.GetKeyDown("space") && PlayerPrefs.GetInt("pause") == 1)
        {
            PlayerPrefs.SetInt("pause", 0);
        }
        //if (fStartTime == 0)
        //{
        //    fStartTime = Time.time;
        //}
        //if (Time.time - fStartTime > 0.1f)
        //{
        //    if (RenderSettings.skybox == BLUE)
        //    {
        //        RenderSettings.skybox = RED;
        //    }
        //    else
        //    {
        //        RenderSettings.skybox = BLUE;
        //    }
        //    fStartTime = 0;
        //}
        //RenderSettings.skybox.SetColor("_Color", Color.blue);
        if (Input.touchCount > 0)
        {
            if (PlayerPrefs.GetInt("pause") == 0)
        {
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    if (PlayerPrefs.GetInt("pause") != null)
                   {
                        if (Input.GetTouch(i).phase == TouchPhase.Ended && PlayerPrefs.GetInt("pause") == 0 && level.score > 5)
                        {
                            PlayerPrefs.SetInt("pause", 1);
                        }
                    }
                }
            }
        }


        ShiftPosition();
    }

    public void ResumeGame()
    {
      PlayerPrefs.SetInt("pause", 0);
    }

    public int WorldLocation()
    {
        if (transform.rotation.eulerAngles.y > 179 && transform.rotation.eulerAngles.y < 181 && transform.rotation.eulerAngles.x < 270)
        {
            quadrant = 2;
            return (int)Mathf.Round((180 - transform.eulerAngles.x) / degrees);
        }
        else if (transform.rotation.eulerAngles.x > 270 && transform.rotation.eulerAngles.y > 1 && transform.rotation.eulerAngles.y < 359)
        {
            quadrant = 3;
            return (int)Mathf.Round((540 - transform.eulerAngles.x) / degrees);
        }
        else if (transform.rotation.eulerAngles.x >= 270 && (transform.rotation.eulerAngles.y < 1 || transform.rotation.eulerAngles.y > 359))
        {
            quadrant = 4;
            return (int)Mathf.Round((transform.eulerAngles.x) / degrees);
        }
        else
        { 
            quadrant = 1;
            return (int)Mathf.Round(transform.eulerAngles.x / degrees);
        }
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void ShiftPosition()
    {
        if (fUserOffsetx > tileWidth)
        {
            fUserOffsetx = 0;
        }
        else if (fUserOffsetx < -tileWidth)
        {
            fUserOffsetx = 0;
        }  
        }
    }

       


    


