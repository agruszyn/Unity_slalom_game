using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    private Score level;
    private GameObject Score;
    private Vector3 parkpos = new Vector3(0, 1900, 200);
    private Vector3 attackpos = new Vector3(0, 2070, 200);
    private Vector3 destination;
    private Vector3 lastdetination;
    public float speed = 0.8f;
    public float xdir;
    public float ydir;
    public float zdir;
    public bool inPos = true;
    public bool starthover;
    public int savedlevel;

    // Use this for initialization
    void Start ()
    {
        
        transform.position = parkpos;
        destination = parkpos;
        Score = GameObject.Find("Score");
        level = Score.GetComponent<Score>();
        savedlevel = level.level;
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            if (level.level == 3 && savedlevel == 0)
            {
                destination = attackpos;
                inPos = false;
                starthover = false;
                savedlevel = 1;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            if (inPos == false)
            {
                MoveTo();
            }

            if (inPos == true && destination != parkpos && (Mathf.Sin((Time.timeSinceLevelLoad)) < 0.01f || starthover == true))
            {
                hover();
                starthover = true;
            }
        }
    }


    void hover()
    {
        transform.position = new Vector3(destination.x + Mathf.Sin((Time.timeSinceLevelLoad))*20, destination.y, destination.z);
    }

    void MoveTo()
    {
        if (destination.x - transform.position.x <= speed)
        { xdir = destination.x; }
        else if (destination.x - transform.position.x < 0)
        { xdir = (-1 * speed) + transform.position.x; }
        else
        { xdir = (1 * speed) + transform.position.x; }
        //Debug.Log(transform.position.x == xdir);

        if ((destination.y - transform.position.y) < speed)
        { ydir = destination.y; }
        else if (destination.y - transform.position.y < 0)
        { ydir = (-1 * speed) + transform.position.y; }
        else
        { ydir = (1 * speed) + transform.position.y; }


        if (destination.z - transform.position.z < speed)
        { zdir = destination.z; }
        else if (destination.z - transform.position.z < 0)
        { zdir = (-1 * speed) + transform.position.z; }
        else
        { zdir = (1 * speed) + transform.position.z; }

        if (destination != transform.position)
        {
            transform.position = new Vector3(xdir, ydir, zdir);
        }
        else
            inPos = true;
    }





}
