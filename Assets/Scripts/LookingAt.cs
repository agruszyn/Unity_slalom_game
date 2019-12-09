using UnityEngine;
using System.Collections;

public class LookingAt : MonoBehaviour {

    private Vector3 lookingAt;
    private int layerMask;
    private crosshair myX;
    private GameObject xHair;

    // Use this for initialization
    void Start ()
    {
        xHair = GameObject.Find("crosshair");
        myX = xHair.GetComponent<crosshair>();           
        layerMask = 1 << 5;
    }
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        Ray myRay = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(myRay, out hit, Mathf.Infinity, layerMask))
        {
            hit.transform.SendMessage("HitByRay");
            myX.transform.SendMessage("LookAt");
        }
        else if (myX != null)
        {
            myX.transform.SendMessage("DontLookAt");
        }
	}
}
