using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class canvas : MonoBehaviour {

    private EternalVariables eValues;
    private GameObject eternalWatchdog;
    private GameObject manager;
    private GameObject player;
    private Canvas myCanvas;
    private Vector3 worldPosition;
    private Vector3 worldRotation;
    private Vector2 screenPosition;
    private CanvasScaler myScale;
    private RectTransform myRect;

    // Use this for initialization
    void Start ()
    {
        eternalWatchdog = GameObject.Find("Eternal watchdog");
        eValues = eternalWatchdog.GetComponent<EternalVariables>();
        manager = GameObject.Find("Menu Manager");
        myCanvas = GetComponent<Canvas>();
        myScale = GetComponent<CanvasScaler>();
        myRect = GetComponent<RectTransform>();
        worldPosition = new Vector3(0, 2150f, 900);
        worldRotation = new Vector3(340, 0, 0);
        screenPosition = new Vector2(601.5f,338);
    }
	
	// Update is called once per frame
	void Update ()
    {
	if (eValues.headset == true)
        {
            myScale.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            gameObject.transform.SetParent(manager.transform);
            //player = GameObject.Find("PlayerCharacter");
            //gameObject.transform.SetParent(player.transform);
            myCanvas.renderMode = RenderMode.WorldSpace;
            myRect.localScale = new Vector3(1, 1, 1);
            transform.localPosition = worldPosition;
            transform.localEulerAngles = worldRotation;
        }
    else if (eValues.headset == false)
        {
            gameObject.transform.SetParent(null);
            myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            transform.localPosition = screenPosition;
            myScale.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        }
	}
}
