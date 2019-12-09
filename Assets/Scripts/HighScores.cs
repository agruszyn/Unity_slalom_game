using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScores : MonoBehaviour {

    Text text;

    // Use this for initialization
    void Start ()
    {
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        text.text = "1. " + PlayerPrefs.GetFloat("1") + "\n2. " + PlayerPrefs.GetFloat("2") + "\n3. " + PlayerPrefs.GetFloat("3") + "\n4. " + PlayerPrefs.GetFloat("4") + "\n5. " + PlayerPrefs.GetFloat("5");

    }
}
