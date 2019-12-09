using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EternalVariables : MonoBehaviour {

    //private Cardboard vrMode;
    private GameObject cardboard;
    public bool headset;
    public int lastScene;
    private GameObject eVariables;
    public bool test = false;
    private static EternalVariables instance = null;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        test = ! test;
    }

    // Use this for initialization
    void Start ()
    {
        PlayerPrefs.SetInt("pause", 0);
        headset = false;
        cardboard = GameObject.Find("Cardboard");
        //vrMode = cardboard.GetComponent<Cardboard>();
        //vrMode.VRModeEnabled = false;
        DontDestroyOnLoad(transform.gameObject);
        lastScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("VRmode", 0);
    }


    public void SetVR()
    {
        headset =! headset;
        PlayerPrefs.SetInt("VRmode", headset ? 1:0);
        //vrMode.VRModeEnabled = headset;
    }

    // Update is called once per frame
    void Update ()
    {

        if (lastScene != SceneManager.GetActiveScene().buildIndex)
        {
            cardboard = GameObject.Find("Cardboard");
            //vrMode = cardboard.GetComponent<Cardboard>();
            //vrMode.VRModeEnabled = headset;
        }



        if (SceneManager.GetActiveScene().isLoaded)
        {
            lastScene = SceneManager.GetActiveScene().buildIndex;
        }
    }
}
