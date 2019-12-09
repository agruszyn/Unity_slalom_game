using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class crosshair : MonoBehaviour {

    private SpriteRenderer mySprite;
    private bool vrMode;

    // Use this for initialization
    void Start ()
    {

        mySprite = GetComponent<SpriteRenderer>();
        vrMode = PlayerPrefs.GetInt("VRmode") == 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        vrMode = PlayerPrefs.GetInt("VRmode") == 1;
        if (vrMode == true)
        {
            mySprite.enabled = true;
        }
        else if (vrMode == false)
        {
            mySprite.enabled = false;
        }


	}

    public void LookAt()
    {
        mySprite.color = new Color(255, 255, 255, 255);
    }
    public void DontLookAt()
    {
        mySprite.color = new Color(255, 0, 0, 255);
    }

}
