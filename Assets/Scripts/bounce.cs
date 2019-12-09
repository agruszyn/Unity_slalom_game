using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour {

    public bool jump = false;
    public bool sway = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {


   }
    private void FixedUpdate()
    {
        move();
    }



    void move()
    {
        if (jump == true)
        { transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin((Time.timeSinceLevelLoad * 4)) * 0.3f, transform.position.z); }
        if (sway == true)
        { transform.position = new Vector3(transform.position.x + Mathf.Sin((Time.timeSinceLevelLoad * 4)) * 0.3f, transform.position.y, transform.position.z); }
    }
}
