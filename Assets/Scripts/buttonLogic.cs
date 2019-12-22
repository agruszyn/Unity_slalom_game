using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonLogic : MonoBehaviour {

    private Button button;
    private EternalVariables eVariables;
    private GameObject eWatchdog;
    private float fStartTime;
    private Text myText;
    private menu myMenu;
    private GameObject mainMenu;
    private bool beingHit;

    // Use this for initialization
    void Start ()
    {
        beingHit = false;
        fStartTime = 0;
        button = GetComponent<Button>();
        eWatchdog = GameObject.Find("Eternal watchdog");
        mainMenu = GameObject.Find("Menu Manager");
        myMenu = mainMenu.GetComponent<menu>();
        eVariables = eWatchdog.GetComponent<EternalVariables>();
        myText = GetComponentInChildren<Text>();
        button.onClick.AddListener(myFunctionForOnClickEvent);  // <-- you assign a method to the button OnClick event here
    }
    void HitByRay()
    {
        beingHit = true;

        if (fStartTime == 0)
        {
            fStartTime = Time.time;
        }
        else
        {
            myText.color = new Color(255, 0, 0, 255);
            myText.fontSize = 20 + (int)((Time.time - fStartTime) * 10);
        }

        if (Time.time - fStartTime > 2f)
        {
            if (gameObject.name == "VR Mode")
            {
                eVariables.SetVR();
            }
            else if (gameObject.name == "Start Game")
            {
                myMenu.SetScene(1);
            }
            else if (gameObject.name == "Reset Score")
            {
                PlayerPrefs.DeleteAll();
            }
            else if (gameObject.name == "High Score")
            {
                myMenu.SetScene(2);
            }
            else if (gameObject.name == "Main Menu")
            {
                myMenu.SetScene(0);
            }
            else if (gameObject.name == "Settings")
            {
                myMenu.SetScene(3);
            }

            myText.fontSize = 20;
            fStartTime = 0;
            myText.color = new Color(255, 255, 255, 255);
        }
    }

    void myFunctionForOnClickEvent()
    {
        // your code goes here
        if (gameObject.name == "VR Mode")
        {
            eVariables.SetVR();
        }
        else if (gameObject.name == "Start Game")
        {    
            myMenu.SetScene(1);
        }
        else if (gameObject.name == "Reset Score")
        {
            PlayerPrefs.DeleteAll();
        }
        else if (gameObject.name == "High Score")
        {
            myMenu.SetScene(2);
        }
        else if (gameObject.name == "Main Menu")
        {
            myMenu.SetScene(0);
        }
        else if (gameObject.name == "Settings")
        {
            myMenu.SetScene(3);
        }
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (beingHit == false)
        {
            myText.fontSize = 20;
            fStartTime = 0;
            myText.color = new Color(255, 255, 255, 255);
        }
        beingHit = false;
    }

    
}
