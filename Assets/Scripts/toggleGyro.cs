using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class toggleGyro : MonoBehaviour {

    private Button button;
    private EternalVariables eVariables;
    private GameObject eWatchdog;
    private int toggle;
    private Text myText;

    // Use this for initialization
    void Start ()
    {
        button = GetComponent<Button>();
        myText = GetComponentInChildren<Text>();
        button.onClick.AddListener(myFunctionForOnClickEvent);  // <-- you assign a method to the button OnClick event here
    }
   

    void myFunctionForOnClickEvent()
    {
        toggle = PlayerPrefs.GetInt("UseGyro");
        if (toggle == 1)
        { toggle = 0; }
        else
        { toggle = 1; }
        PlayerPrefs.SetInt("UseGyro", toggle);
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerPrefs.GetInt("UseGyro") == 1)
            myText.text = "GYRO: ENABLED";
        else
            myText.text = "GYRO: DISABLED";

    }


}
