using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Falling : MonoBehaviour {

    public bool latch;
    public Vector3 destin;

    private Vector3 destination;
    public Vector3 test;
    private Vector3 lastdetination;
    public float factor = 0.08f;
    public float xdir;
    public float ydir;
    public float zdir;
    public bool moving = false;
    public float speedx;
    public float speedz;
    public float gravity;
    MeshRenderer meshR;
    public int layer;

    // Use this for initialization
    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            test = transform.localPosition;

            if (meshR.enabled == false)
            {
                latch = false;
                moving = false;
            }

            if (latch)
            {
                MoveTo(destin);
            }
        }
    }


    public void MoveTo(Vector3 destination)
    {
        latch = true;
        if (moving == false)

        {
             speedx = Mathf.Sqrt(Mathf.Abs(destination.x - transform.localPosition.x)) * factor;
             speedz = Mathf.Sqrt(Mathf.Abs(destination.z - transform.localPosition.z)) * factor;
             gravity = Mathf.Sqrt(Mathf.Abs(destination.y - transform.localPosition.y)) * factor;
            moving = true;
        }

        if (Mathf.Abs(destination.x - transform.localPosition.x) <= speedx)
        { xdir = destination.x; }
        else if (destination.x - transform.localPosition.x < 0)
        { xdir = (-1 * speedx) + transform.localPosition.x; }
        else
        { xdir = (1 * speedx) + transform.localPosition.x; }

        if (Mathf.Abs(destination.y - transform.localPosition.y) < gravity)
        { ydir = destination.y; }
        else if (destination.y - transform.localPosition.y < 0)
        { ydir = (-1 * gravity) + transform.localPosition.y; }
        else
        { ydir = (1 * gravity) + transform.localPosition.y; }


        if (Mathf.Abs(destination.z - transform.localPosition.z) < speedz)
        { zdir = destination.z; }
        else if (destination.z - transform.localPosition.z < 0)
        { zdir = (-1 * speedz) + transform.localPosition.z; }
        else
        { zdir = (1 * speedz) + transform.localPosition.z; }

        if (destination != transform.localPosition)
        {
            transform.localPosition = new Vector3(xdir, ydir, zdir);
        }
        else
        { moving = false;
        latch = false; }
    }

}
