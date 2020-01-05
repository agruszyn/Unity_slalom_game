using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
//using UnityEngine.Monetization;

public class EternalVariables : MonoBehaviour
{

    //private Cardboard vrMode;
    private GameObject cardboard;
    public bool headset;
    public int lastScene;
    private GameObject eVariables;
    public bool test = false;
    private static EternalVariables instance = null;
    string gameId = "3407722";
    string videoId = "video";
    bool testMode = false;
    bool enablePerPlacementLoad = true;
    public string placementId = "Banner";
    public int adRotation;

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
        test = !test;
    }

    // Use this for initialization
    void Start()
    {
        testMode = false;
        adRotation = 0;
        Advertisement.Initialize(gameId, testMode);
        PlayerPrefs.SetInt("pause", 0);
        headset = false;
        cardboard = GameObject.Find("Cardboard");
        //vrMode = cardboard.GetComponent<Cardboard>();
        //vrMode.VRModeEnabled = false;
        DontDestroyOnLoad(transform.gameObject);
        lastScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("VRmode", 0);
        if (!PlayerPrefs.HasKey("UseGyro"))
        {
            if (SystemInfo.supportsGyroscope == true)
            {
                PlayerPrefs.SetInt("UseGyro", 1);
            }
            else { PlayerPrefs.SetInt("UseGyro", 0); }
        }
        //Advertisement.Load(placementId);
        //Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        //Advertisement.Load(videoId);

    }


    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            Debug.Log("waiting for banner. . .");
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("show ad");
        Advertisement.Banner.Show(placementId);

    }

    IEnumerator ShowVideoWhenReady()
    {
        while (!Advertisement.IsReady(videoId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("show ad");
        Advertisement.Show(videoId);
    }



    public void SetVR()
    {
        headset = !headset;
        PlayerPrefs.SetInt("VRmode", headset ? 1 : 0);
        //vrMode.VRModeEnabled = headset;
    }

    // Update is called once per frame
    void Update()
    {

        //      if (lastScene != SceneManager.GetActiveScene().buildIndex)
        //      {
        //          cardboard = GameObject.Find("Cardboard");
        //vrMode = cardboard.GetComponent<Cardboard>();
        //vrMode.VRModeEnabled = headset;
        //      }



        if (SceneManager.GetActiveScene().isLoaded && lastScene != SceneManager.GetActiveScene().buildIndex)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2 && lastScene == 1)
            {
                if (adRotation < 3)
                {
                    //Advertisement.Load(placementId);
                    Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
                    StartCoroutine(ShowBannerWhenReady());
                    adRotation += 1;
                }
                else if (adRotation == 3)
                {
                    StartCoroutine(ShowVideoWhenReady());
                    //Advertisement.Load(videoId);
                    adRotation += 1;
                }
                else if (adRotation >= 4)
                {
                    //Advertisement.Load(placementId);
                    Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
                    StartCoroutine(ShowBannerWhenReady());
                    adRotation = 1;
                }
            }
                lastScene = SceneManager.GetActiveScene().buildIndex;
            }
        }
}
